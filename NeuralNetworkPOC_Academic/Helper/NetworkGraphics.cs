using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NeuralNetworkPOC
{
    public static class NetworkGraphics
    {
        public static List<string> OutputNames { get; set; }
        public static List<string> InputNames { get; set; }

        const int neuronWidth = 30;
        const int neuronDistance = 50;
        const int layerDistance = 75;
        const int fontSize = 8;
        const float scalateDendritesDrawingWidth = 0.5F;
        const int headRoom = 25;

        public static void ToTreeView(TreeView t, NeuralNetwork nn)
        {
            t.Nodes.Clear();
             
            TreeNode root = new TreeNode("NeuralNetwork");

            nn.Layers.ForEach((layer) =>
            {
                TreeNode lnode = new TreeNode("Layer");

                for (int i = layer.NeuronCount - 1; i >= 0; i--)
                {
                    TreeNode nnode = new TreeNode("Neuron");
                    nnode.Nodes.Add("Value: " + layer.NeuronValues[0, i].ToString());
                    nnode.Nodes.Add("Bias: " + layer.Bias[0, i].ToString());

                    if (layer.Name != null && layer.Name.Length > 0)
                        nnode.Nodes.Add("Layer: " + layer.Name);

                    try
                    {
                        if(layer.NeuronNames.Count > 0 && layer.NeuronNames[i] != null && layer.NeuronNames[i].Length > 0)
                            nnode.Nodes.Add("Name: " + layer.NeuronNames[i]);
                    }
                    catch (Exception) { }

                    for (int j = layer.NextLayerNeuronCount - 1; j >= 0; j--)
                    {
                        TreeNode dnode = new TreeNode("Dendrite");
                        dnode.Nodes.Add("Weight: " + layer.Synapse[i,j].ToString());
                        nnode.Nodes.Add(dnode);
                    }

                    lnode.Nodes.Add(nnode);
                }

                root.Nodes.Add(lnode);
            });

            //root.ExpandAll();
            t.Nodes.Add(root);
        }

        public static void ToPictureBox(PictureBox p, NeuralNetwork nn, int X, int Y)
        {
            Bitmap b = CreateNetworkImage(nn);
            p.Image = b;
        }

        private static Bitmap CreateNetworkImage(NeuralNetwork nn)
        {
            int height = nn.Layers.Count * layerDistance;
            int width2 = nn.GetWidestLayerNeuronCount() * neuronDistance + neuronDistance; // p.Width;
            Bitmap b = new Bitmap(width2, height);
            Graphics g = Graphics.FromImage(b);

            g.FillRectangle(Brushes.Transparent, g.ClipBounds);

            DrawDendrites(nn, g, width2 / 2 + headRoom / 2, headRoom);
            DrawNeurons(nn, g, width2 / 2 + headRoom/2, headRoom);
            b.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return b;
        }

        private static void DrawDendrites(NeuralNetwork nn, Graphics g, int X, int Y)
        {
            Y += layerDistance;

            for (int l = 1; l < nn.Layers.Count; l++)
            {
                //List<Neuron> neurons = nn.Layers[l].Neurons;
                //List<Neuron> prevNeurons = nn.Layers[l - 1].Neurons;

                int x = X - (neuronDistance * (nn.Layers[l].NeuronCount / 2));

                for (int n = 0; n < nn.Layers[l].NeuronCount; n++)
                {
                    //Neuron neuron = neurons[n];
                    Point neuronCenter = new Point(x + neuronWidth / 2, Y + neuronWidth / 2);

                    //Previous Layer Neuron Positions
                    int xPrev = X - (neuronDistance * (nn.Layers[l - 1].NeuronCount / 2));
                    Pen pen;

                    for (int j = 0; j < nn.Layers[l - 1].NeuronCount; j++)
                    {
                        Point fromPrevNeuronCenter = new Point(xPrev + (j * neuronDistance) + neuronWidth / 2, Y - layerDistance + neuronWidth / 2);
                        
                        if (nn.Layers[l - 1].Synapse[j, n] > 0)
                            pen = new Pen(Color.Green, (float)nn.Layers[l - 1].Synapse[j,n]* scalateDendritesDrawingWidth);
                        else
                            pen = new Pen(Color.Red, (float)(-nn.Layers[l - 1].Synapse[j, n]) * scalateDendritesDrawingWidth);

                        g.DrawLine(pen, fromPrevNeuronCenter, neuronCenter);
                    }

                    x += neuronDistance;
                };

                Y += layerDistance;
            }

        }

        private static void DrawNeurons(NeuralNetwork nn, Graphics g, int X, int y)
        {
            for (int l = 0; l < nn.Layers.Count; l++)
            {
                //Layer layer = nn.Layers[l];
                //List<Neuron> neurons = layer.Neuron;

                int x = X - (neuronDistance * (nn.Layers[l].NeuronCount / 2));

                try
                {
                    for (int n = 0; n < nn.Layers[l].NeuronCount; n++)
                    {
                        //Neuron neuron = neurons[n];
                        g.FillEllipse(Brushes.WhiteSmoke, x, y, neuronWidth, neuronWidth);
                        g.DrawEllipse(Pens.Gray, x, y, neuronWidth, neuronWidth);
                        //TODO: ESTO ESTA MAL!!
                        g.DrawString(nn.Layers[l].NeuronValues[0,n].ToString("0.00"), new Font("Arial", fontSize), Brushes.Black, x + 2, y + (neuronWidth / 2) - 5);
                        x += neuronDistance;
                    }
                }
                catch (Exception) { }




                y += layerDistance;
            }
        }

        public static void ExportTreeViewToXml(TreeView tv, string filename)
        {
            StreamWriter sr = new StreamWriter(filename, false, System.Text.Encoding.UTF8);
            sr.WriteLine("<" + tv.Nodes[0].Text + ">");

            foreach (TreeNode node in tv.Nodes)
                SaveNode(node.Nodes, sr);

            //Close the root node
            sr.WriteLine("</" + tv.Nodes[0].Text + ">");
            sr.Close();
        }

        private static void SaveNode(TreeNodeCollection tnc, StreamWriter sr)
        {
            foreach (TreeNode node in tnc)
            {
                if (node.Nodes.Count > 0)
                {
                    sr.Write("<" + node.Text + ">");
                    SaveNode(node.Nodes,sr);
                    sr.WriteLine("</" + node.Text + ">");
                }
                else
                    sr.Write(node.Text);
            }
        }

    }
}

