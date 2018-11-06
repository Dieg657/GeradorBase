using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void renomearArquivos(string[] nomesDeArquivo, string path, int qtdImgTreino)
        {
            int tam = nomesDeArquivo.Length;

            if (Directory.Exists(path + "\\Teste"))
            {
                Directory.Delete(path + "\\Teste",true);
            }
            if (Directory.Exists(path + "\\Treino"))
            {
                Directory.Delete(path + "\\Treino",true);
            }

            if (!Directory.Exists(path + "\\Teste"))
            {
                Directory.CreateDirectory(path + "\\Treino");
                string treino = path + "\\Treino";
                for (int i = 0; i < qtdImgTreino; i++)
                {
                    File.Copy(nomesDeArquivo[i], treino + "\\" + (i + 1) + ".png");
                }
            }

            if (!Directory.Exists(path + "\\Teste"))
            {
                Directory.CreateDirectory(path + "\\Teste");
                string teste = path + "\\Teste";
                for (int i = qtdImgTreino; i < tam; i++)
                {
                    File.Copy(nomesDeArquivo[i], teste + "\\" + (i + 1) + ".png");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
             * 1 - Verificar se o diretório existe
             * 2 - Verificar se a pasta existe
             * 3 - Se existir, iterar na mesma, renomeando o arquivo para outro diretório
             */

            if (Directory.Exists(textBox1.Text))
            {

                int pasteNum = 1, pasteNumMax = Convert.ToInt32(textBox3.Text);
                int qtdImgTreino = Convert.ToInt32(textBox2.Text);
                int pasteNumFile = 1;

                while (pasteNum <= pasteNumMax)
                {
                    
                    //Itera nas pastas
                    if(Directory.Exists(textBox1.Text + "\\s" + pasteNum))
                    {
                        Directory.SetCurrentDirectory(textBox1.Text + "\\s" + pasteNum);
                        while(Directory.Exists(Directory.GetCurrentDirectory() + "\\00" + pasteNumFile))
                        {
                            //textBox2.AppendText(Directory.GetCurrentDirectory() + "\\00" + pasteNumFile + "\n");
                            string path = Directory.GetCurrentDirectory() + "\\00" + pasteNumFile;

                            string[] filesToRename = Directory.GetFiles(path,"*.png");

                            renomearArquivos(filesToRename, Directory.GetCurrentDirectory(), qtdImgTreino);
                           
                            pasteNumFile += 1;
                        }
                        //Reinicia o contador de pasta interna
                        pasteNumFile = 1;
                    }
                    pasteNum += 1;
                }
            }
            else
            {
                MessageBox.Show("Diretório não encontrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }
    }
}
