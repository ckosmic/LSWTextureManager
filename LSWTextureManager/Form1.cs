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

namespace LSWTextureManager
{
	public partial class Form1 : Form
	{
		private GHGParameters ghg;

		public Form1() {
			InitializeComponent();
			textBox1.Text = "No file currently open.";
		}

		// Load GHG button
		private void button1_Click(object sender, EventArgs e) {
			openFileDialog1.InitialDirectory = Properties.Settings.Default.ghgPath;
			if (openFileDialog1.ShowDialog() == DialogResult.OK) {
				Program.LoadModel(openFileDialog1.FileName, out ghg);
				textBox1.Text = Path.GetFileName(openFileDialog1.FileName) + " Information:\r\n\r\n";
				textBox1.Text += "Texture count: " + ghg.texCount + "\r\n";
				for (int i = 0; i < ghg.texCount; i++) {
					textBox1.Text += "Image " + i + " - width: " + ghg.texWidths[i] + ", height: " + ghg.texHeights[i] + ", offset: " + ghg.texOffsets[i].ToString("X8") + "\r\n";
				}

				Properties.Settings.Default.ghgPath = Path.GetDirectoryName(openFileDialog1.FileName);
				Properties.Settings.Default.Save();
			}
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e) {

		}

		// Load DDS button
		private void button2_Click(object sender, EventArgs e) {
			openFileDialog2.InitialDirectory = Properties.Settings.Default.ddsPath;
			if (openFileDialog2.ShowDialog() == DialogResult.OK) {
				Properties.Settings.Default.ddsPath = Path.GetDirectoryName(openFileDialog2.FileName);
				Properties.Settings.Default.Save();
			}
		}

		// Replace button
		private void button3_Click(object sender, EventArgs e) {
			if (Path.GetExtension(openFileDialog1.FileName).ToLower() == ".ghg") {
				if (Path.GetExtension(openFileDialog2.FileName).ToLower() == ".dds") {
					bool result = Program.OverwriteTexture((int)numericUpDown1.Value, ghg, openFileDialog1.FileName, openFileDialog2.FileName);
					if (result) {
						MessageBox.Show("Successfully replaced texture!", "Success!", MessageBoxButtons.OK);
					}
				} else {
					MessageBox.Show("Please select a .DDS file", "Error", MessageBoxButtons.OK);
				}
			} else {
				MessageBox.Show("Please select a .GHG file", "Error", MessageBoxButtons.OK);
			}
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {

		}

		// Export button
		private void button4_Click(object sender, EventArgs e) {
			if (Path.GetExtension(openFileDialog1.FileName).ToLower() == ".ghg") {
				saveFileDialog1.InitialDirectory = Properties.Settings.Default.exportPath;
				if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
					File.WriteAllBytes(saveFileDialog1.FileName, ghg.texData[(int)numericUpDown1.Value]);
					Properties.Settings.Default.exportPath = Path.GetDirectoryName(saveFileDialog1.FileName);
					Properties.Settings.Default.Save();
					MessageBox.Show("Successfully exported texture!", "Success!", MessageBoxButtons.OK);
				}
			} else {
				MessageBox.Show("Please select a .GHG file", "Error", MessageBoxButtons.OK);
			}
		}

		private void label4_Click(object sender, EventArgs e) {

		}

		private void Form1_Load(object sender, EventArgs e) {

		}
	}
}
