﻿using System.Text;

namespace ApiPractice.Auth
{
    public class Settings
    {
        internal static string SecretKey = "6ceccd7405ef4b00b2630009be568cfa";
        internal static byte[] GenerateSecretByte() =>
            Encoding.ASCII.GetBytes(SecretKey);
    }
}
