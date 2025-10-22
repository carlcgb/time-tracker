using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chronometre.Forms
{
    public partial class NoteDialog : Form
    {
        private TextBox _noteTextBox;
        private Button _okButton;
        private Button _cancelButton;
        private Label _noteLabel;

        public string Note => _noteTextBox.Text.Trim();

        public NoteDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form properties
            Text = "Add Note";
            Size = new Size(400, 150);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            TopMost = true;

            // Note label
            _noteLabel = new Label
            {
                Text = "Note:",
                Location = new Point(12, 15),
                Size = new Size(100, 23),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Note text box
            _noteTextBox = new TextBox
            {
                Location = new Point(12, 40),
                Size = new Size(360, 23),
                MaxLength = 500
            };

            // OK button
            _okButton = new Button
            {
                Text = "OK",
                Location = new Point(216, 80),
                Size = new Size(75, 23),
                DialogResult = DialogResult.OK,
                UseVisualStyleBackColor = true
            };

            // Cancel button
            _cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(297, 80),
                Size = new Size(75, 23),
                DialogResult = DialogResult.Cancel,
                UseVisualStyleBackColor = true
            };

            // Add controls to form
            Controls.Add(_noteLabel);
            Controls.Add(_noteTextBox);
            Controls.Add(_okButton);
            Controls.Add(_cancelButton);

            // Set tab order
            _noteTextBox.TabIndex = 0;
            _okButton.TabIndex = 1;
            _cancelButton.TabIndex = 2;

            // Set default button
            AcceptButton = _okButton;
            CancelButton = _cancelButton;

            // Event handlers
            _noteTextBox.KeyDown += OnNoteTextBoxKeyDown;
            _okButton.Click += OnOkButtonClick;
            _cancelButton.Click += OnCancelButtonClick;

            // Focus on text box when form loads
            Load += (sender, e) => _noteTextBox.Focus();
        }

        private void OnNoteTextBoxKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.Handled = true;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void OnOkButtonClick(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnCancelButtonClick(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
