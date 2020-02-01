using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Assets.Networking.Messaging
{
    public static class MessageBuilder
    {
        private static JsonSerializerSettings _settings;
        private static ISet<Type> TypesToCallJsonUtilityOn;

        static MessageBuilder()
        {
            _settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new GameObjectConverter()
                }
            };

            TypesToCallJsonUtilityOn = new HashSet<Type>
            {
                typeof(Vector3),
                typeof(Quaternion)
            };
        }

        public static byte[] GetMessage(object obj, Guid guid, MessageStrategy messageStrategy = MessageStrategy.NoHeader)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"Cannot convert null obj to message {nameof(obj)}");
            }

            if (messageStrategy == MessageStrategy.Header)
            {
                return GetMessageWithHeader(obj, guid);
            }

            return GetMessageWithoutHeader(obj, guid);

        }

        private static unsafe byte[] GetMessageWithHeader(object obj, Guid guid)
        {
            var wrapperAsJson = GetMessageWrapperAsJson(obj, guid);
            var lengthAsBytes = BitConverter.GetBytes(wrapperAsJson.Length);

            //Debug assert here to throw a message if the wrapper exceeds 85000 bytes
            //Because if it does, we're in trouble. It will trigger an allocation to the LOH
            //And if there isn't a continuous 85000 byte block in the slab, this will throw a OutOfMemoryException
            Debug.Assert(wrapperAsJson.LongCount() < 85000);

            var messageInBytes = new byte[lengthAsBytes.Length + Encoding.UTF8.GetByteCount(wrapperAsJson)];
            var i = 0;
            for (; i < lengthAsBytes.Length; i++)
            {
                messageInBytes[i] = lengthAsBytes[i];
            }

            var encoder = Encoding.UTF8.GetEncoder();
            int charsOut;
            int bytesUsed;
            bool completed;
            fixed (char* charPtr = wrapperAsJson)
            {
                fixed (byte* bytes = messageInBytes)
                {
                    var offSetPtr = bytes + sizeof(int);
                    encoder.Convert(charPtr, wrapperAsJson.Length, offSetPtr, messageInBytes.Length - i, true,
                                    out charsOut, out bytesUsed, out completed);
                }
            }

            Debug.Assert(charsOut == wrapperAsJson.Length);
            Debug.Assert(bytesUsed == messageInBytes.Length - i);
            Debug.Assert(completed);
            return messageInBytes;
        }

        private static byte[] GetMessageWithoutHeader(object obj, Guid guid)
        {
            var wrapperJson = GetMessageWrapperAsJson(obj, guid);
            // Generally, we want our UDP messages to be a single packet.
            // This assert makes a note that we should keep ourselves under 500 bytes.
            Debug.Assert(Encoding.UTF8.GetBytes(wrapperJson).Length < 500);
            return Encoding.UTF8.GetBytes(wrapperJson);
        }

        private static string GetMessageWrapperAsJson(object obj, Guid guid)
        {
            if (obj == null)
            {
                UnityEngine.Debug.LogWarning("Object being passed in is null");
            }

            UnityEngine.Debug.Log($"Trying to convert {obj.GetType()} into message");
            var messageWrapper = new MessageWrapper(obj, guid);

            try
            {
                return JsonConvert.SerializeObject(messageWrapper, _settings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
