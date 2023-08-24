using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class WordSuggestionsForm : Form
	{
		private int prevIndex;

		private int checkIndex;

		private string[] words = new string[0];

		public EventHandler OnWordChange;

		private TextBox textBoxNotInDictionary;

		private Label label1;

		private Label label2;

		private ListBox listBoxSuggestions;

		private Button buttonChange;

		private Button buttonCancel;

		private Container components;

		private string wrongWord = "";

		private int wrongWordStartIndex = -1;

		private TextBox textBox;

		public string WrongWord
		{
			get
			{
				return wrongWord;
			}
			set
			{
				string text2 = wrongWord = (textBoxNotInDictionary.Text = value);
			}
		}

		public int WrongWordStartIndex
		{
			get
			{
				return wrongWordStartIndex;
			}
			set
			{
				wrongWordStartIndex = value;
			}
		}

		public TextBox TextData
		{
			get
			{
				return textBox;
			}
			set
			{
				textBox = value;
				words = textBox.Text.Split(' ');
				Check();
			}
		}

		public string CorrectWord => textBoxNotInDictionary.Text;

		public WordSuggestionsForm()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			textBoxNotInDictionary = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			listBoxSuggestions = new System.Windows.Forms.ListBox();
			buttonChange = new System.Windows.Forms.Button();
			buttonCancel = new System.Windows.Forms.Button();
			SuspendLayout();
			textBoxNotInDictionary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			textBoxNotInDictionary.Location = new System.Drawing.Point(8, 40);
			textBoxNotInDictionary.MaxLength = 64;
			textBoxNotInDictionary.Name = "textBoxNotInDictionary";
			textBoxNotInDictionary.Size = new System.Drawing.Size(328, 20);
			textBoxNotInDictionary.TabIndex = 0;
			textBoxNotInDictionary.Text = "";
			label1.Location = new System.Drawing.Point(8, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(176, 16);
			label1.TabIndex = 1;
			label1.Text = "Not in Disctionary:";
			label2.Location = new System.Drawing.Point(8, 72);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(176, 16);
			label2.TabIndex = 2;
			label2.Text = "Suggestions:";
			listBoxSuggestions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			listBoxSuggestions.Location = new System.Drawing.Point(8, 96);
			listBoxSuggestions.Name = "listBoxSuggestions";
			listBoxSuggestions.Size = new System.Drawing.Size(328, 158);
			listBoxSuggestions.TabIndex = 3;
			listBoxSuggestions.DoubleClick += new System.EventHandler(listBoxSuggestions_DoubleClick);
			listBoxSuggestions.SelectedIndexChanged += new System.EventHandler(listBoxSuggestions_SelectedIndexChanged);
			buttonChange.Location = new System.Drawing.Point(352, 40);
			buttonChange.Name = "buttonChange";
			buttonChange.Size = new System.Drawing.Size(80, 24);
			buttonChange.TabIndex = 5;
			buttonChange.Text = "&Change";
			buttonChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonChange.Click += new System.EventHandler(buttonChange_Click);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(352, 264);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(80, 24);
			buttonCancel.TabIndex = 6;
			buttonCancel.Text = "&Done";
			buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			base.AcceptButton = buttonChange;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(472, 293);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonChange);
			base.Controls.Add(listBoxSuggestions);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxNotInDictionary);
			base.Name = "WordSuggestionsForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "WordSuggestionsForm";
			base.Activated += new System.EventHandler(WordSuggestionsForm_Activated);
			ResumeLayout(false);
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonAddToDictionary_Click(object sender, EventArgs e)
		{
		}

		private new bool Load(string word)
		{
			listBoxSuggestions.Items.Clear();
			return false;
		}

		private void WordSuggestionsForm_Load(object sender, EventArgs e)
		{
		}

		private void WordSuggestionsForm_Activated(object sender, EventArgs e)
		{
		}

		private void Change()
		{
			textBox.Select(WrongWordStartIndex, WrongWord.Length);
			textBox.SelectedText = CorrectWord;
		}

		public void CheckIt(string word)
		{
			Load(word);
		}

		public bool IsBadWord(string word)
		{
			return false;
		}

		private bool Check()
		{
			if (checkIndex >= words.Length)
			{
				return false;
			}
			int num = prevIndex;
			while (checkIndex < words.Length && words[checkIndex++].Length <= 0)
			{
				num++;
			}
			return true;
		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			if (textBoxNotInDictionary.Text.Trim().Length > 0)
			{
				Change();
				if (!Check())
				{
					Close();
				}
			}
		}

		private void listBoxSuggestions_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxSuggestions.SelectedItem != null)
			{
				textBoxNotInDictionary.Text = listBoxSuggestions.SelectedItem.ToString();
			}
		}

		private void listBoxSuggestions_DoubleClick(object sender, EventArgs e)
		{
			buttonChange_Click(sender, e);
		}
	}
}
