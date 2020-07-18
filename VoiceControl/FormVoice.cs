﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;//Sesi Tanıma
using System.Speech.Synthesis;
using System.Media;
using System.Diagnostics;
using System.Globalization;//totitlecase
using System.Threading;

namespace VoiceControl
{
    public partial class FormVoice : Form
    {
        SpeechRecognitionEngine rec = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();
        SoundPlayer player = new SoundPlayer();
        string on =Application.StartupPath+"\\SiriOn.wav";
        string understood =Application.StartupPath+"\\SiriUnderstood.wav";
        
        private void StartingVoice()
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                Choices choices = new Choices();
                string[] words = { "hello", "open paint", "open word", "open google", "open youtube", "what time is it","how are you"
                ,"hey assistant","exit the application","stop listen"};
                choices.Add(words);
                Grammar grammar = new Grammar(new GrammarBuilder(choices));
                //Grammar grammar = new DictationGrammar();
                rec.LoadGrammar(grammar);
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
                rec.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Speechrecognized);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Lutfen bilgisayarinizin dilini ingilizceye cevirip tekrar deneyin", "Error!");
                Application.Exit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(),"Error!");
            }


       
        }


        public FormVoice()
        {
            InitializeComponent();
        }

        private void Speechrecognized(object sender, SpeechRecognizedEventArgs e)
        {
            timer1.Enabled = true;
            bool control = false;
            string result = e.Result.Text;
            string newy = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result);
            richTextBox1.AppendText("You: " + newy + Environment.NewLine);
            Random rd = new Random();
            if (result == "hey assistant")
            {
                int x = rd.Next(1,3);
                if (x == 1)
                {
                    //timer1.Enabled = true;
                    control = true;
                    result = "I'm listening to you,you can use these words:";
                    richTextBox1.AppendText("Assistant: " + result + Environment.NewLine);
                    player.SoundLocation = understood;
                    player.Play();
                    speech.SpeakAsync(result);
                    richTextBox1.AppendText("\nHello, Open paint, Open word, Open google, Open youtube, What time is it, How are you," +
                    "Hey assistant, Exit the application, Stop listen, Listen to me...");
                }
                else
                {
                    result = "Hi,how can I help u";
                }
                
             
            }
            if (result == "hello")
            {
                
                int i=rd.Next(1, 3);
                if (i == 1)
                {
                    result = "Hello,How are you";
                }
                else
                {
                    result = "What can I do for you?";
                }

            }
            if(result == "how are you")
            {
                //not complete
            }
            if(result == "what time is it" )
            {
                result = "It is" + DateTime.Now.ToLongTimeString();
            }
            if(result == "open youtube" )
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/?gl=TR");
                result = "Opening youtube";
            }
           
            if(result == "open google" )
            {
                result = "Opening google";
                System.Diagnostics.Process.Start("https://www.google.com.tr");
            }
            if(result == "open paint")
            {
                result = "Opening paint";
                System.Diagnostics.Process.Start("MSpaint.Exe");
                
            }
            if(result == "open word")
            {
                //not complete
                result = "Opening word";
                
            }
            if(result == "exit the application")
            {
                result = "Okey closing it";
                Application.Exit();
                
            }
            if(result == "what time is it")
            {
                result = "It is" + DateTime.Now.ToLongTimeString();
            }
            if(result == "what time is it")
            {
                result = "It is" + DateTime.Now.ToLongTimeString();
            }
            if(result == "stop listen")
            {
                result = "I stopped listening to you";
                //speech.SpeakAsyncCancelAll();
                rec.RecognizeAsyncCancel(); //tanımayı cancel et
                pictureBox1.Enabled = true;
                labelActivate.Visible = true;

            }
            if (!control)
            {
                player.SoundLocation = understood;
                player.Play();
                speech.SpeakAsync(result);
                richTextBox1.AppendText("Assistant: " + result + Environment.NewLine);
            }
            timer1.Enabled = false;
            Thread.Sleep(100);
            //1000=>1 seconds 

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            player.SoundLocation = on;
            player.Play();
            StartingVoice();
            pictureBox1.Enabled = false;
            labelActivate.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product Product = new Product();
            Product.Show();
           
            //FormVoice.ActiveForm.Close();
            this.Hide();
        }
    }
}