Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography

Public Class Encryption
    Public Function encrypt(ByVal encryptString As String) As String
        Dim EncryptionKey As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(encryptString)

        Using encryptor As Aes = Aes.Create()
            Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)

            Using ms As MemoryStream = New MemoryStream()

                Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using

                encryptString = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using

        Return encryptString
    End Function

    Public Function decrypt(ByVal cipherText As String) As String
        Dim EncryptionKey As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        cipherText = cipherText.Replace(" ", "+")
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)

        Using encryptor As Aes = Aes.Create()
            Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)

            Using ms As MemoryStream = New MemoryStream()

                Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using

                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using

        Return cipherText
    End Function

End Class
