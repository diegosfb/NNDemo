using System;
using System.Windows.Forms;

namespace NeuralNetworkPOC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //ExecuteSimplifiedNeuralNetwork.Execute();
            Application.Run(new FormNetwork());
            
        }
    }
}
