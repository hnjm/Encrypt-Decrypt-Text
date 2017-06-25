using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace EncyptandDecryptText
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            desObj = Rijndael.Create();

        }
        string cipherData;
        byte[] cipherbytes;
        byte[] plainbytes;
        byte[] plainbytes2;
        byte[] plainkey;

        SymmetricAlgorithm desObj;

        private void EncryptBtn_Click(object sender, EventArgs e)
        {
            cipherData = plaintxt.Text;
            plainbytes = Encoding.ASCII.GetBytes(cipherData);
            plainkey = Encoding.ASCII.GetBytes("0123456789abcdef");
            desObj.Key = plainkey;

            desObj.Mode = CipherMode.CBC;
            desObj.Padding = PaddingMode.PKCS7;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desObj.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(plainbytes, 0, plainbytes.Length);
            cs.Close();
            cipherbytes = ms.ToArray();
            ms.Close();
            EncyptTxt.Text = Encoding.ASCII.GetString(cipherbytes);
        }

        private void DecryptBtn_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream(cipherbytes);
            CryptoStream cs = new CryptoStream(ms, desObj.CreateDecryptor(), CryptoStreamMode.Read);
            cs.Read(cipherbytes, 0, cipherbytes.Length);
            plainbytes2 = ms.ToArray();
            cs.Close();
            ms.Close();
            DecryptTxt.Text = Encoding.ASCII.GetString(plainbytes2);
        }
    }
}
