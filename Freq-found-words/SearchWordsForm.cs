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
using System.Text.RegularExpressions;

namespace Freq_found_words
{
    public partial class SearchWords : Form
    {
        public SearchWords()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string filePath;
            string[] readFile;
            Dictionary<string, int> resultMap = new Dictionary<string, int>();

            if (inputTextBox.Text.Length != 0)
            {
                filePath = inputTextBox.Text;
            }
            else
            {
                outputTextBox.Text = "Please. enter path ro input file";
                return;
            }

            if (File.Exists(filePath))
            {
                readFile = File.ReadAllLines(filePath);
            }
            else
            {
                outputTextBox.Text = "File is not exist";
                return;
            }

            resultMap = CountSameWords(readFile);

            outputTextBox.Text = "";
            foreach (KeyValuePair<string, int> wordQuantityPair in resultMap.OrderByDescending(key => key.Value))
            {
                outputTextBox.AppendText(wordQuantityPair.Key + " " + wordQuantityPair.Value + "\n");
            }
        }

        private Dictionary<string, int> CountSameWords (string[] readFile)
        {
            Dictionary<string, int> wordsMap = new Dictionary<string, int>();

            foreach (string str in readFile)
            {
                string _newString = Regex.Replace(str, "[-.?!)(,:]", "").ToLower();
                string[] _words = _newString.Split(' ');
                foreach (string word in _words)
                {
                    if (!wordsMap.ContainsKey(word))
                    {
                        wordsMap.Add(word, 1);
                    }
                    else
                    {
                        wordsMap[word]++;
                    }
                }
            }

            return wordsMap;
        }
    }
}
