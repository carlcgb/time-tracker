using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chronometre.Forms
{
    public partial class StartDialog : Form
    {
        private TextBox _notesTextBox;
        private Button _okButton;
        private Button _cancelButton;
        private Label _notesLabel;

        public string Notes => _notesTextBox.Text.Trim();

        public StartDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form properties
            Text = "Start Session";
            Size = new Size(400, 150);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            TopMost = true;

            // Notes label
            _notesLabel = new Label
            {
                Text = "Notes (optional):",
                Location = new Point(12, 15),
                Size = new Size(100, 23),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Notes text box
            _notesTextBox = new TextBox
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
            Controls.Add(_notesLabel);
            Controls.Add(_notesTextBox);
            Controls.Add(_okButton);
            Controls.Add(_cancelButton);

            // Set tab order
            _notesTextBox.TabIndex = 0;
            _okButton.TabIndex = 1;
            _cancelButton.TabIndex = 2;

            // Set default button
            AcceptButton = _okButton;
            CancelButton = _cancelButton;

            // Event handlers
            _notesTextBox.KeyDown += OnNotesTextBoxKeyDown;
            _okButton.Click += OnOkButtonClick;
            _cancelButton.Click += OnCancelButtonClick;

            // Focus on text box when form loads
            Load += (sender, e) => _notesTextBox.Focus();
        }

        private void OnNotesTextBoxKeyDown(object? sender, KeyEventArgs e)
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
