namespace SystemServer
{
    partial class FormClient
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SendJoke = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SendJoke
            // 
            this.SendJoke.Location = new System.Drawing.Point(82, 43);
            this.SendJoke.Name = "SendJoke";
            this.SendJoke.Size = new System.Drawing.Size(173, 94);
            this.SendJoke.TabIndex = 6;
            this.SendJoke.Text = "Продать слона";
            this.SendJoke.UseVisualStyleBackColor = true;
            this.SendJoke.Click += new System.EventHandler(this.SendJoke_Click);
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 225);
            this.Controls.Add(this.SendJoke);
            this.Name = "FormClient";
            this.Text = "Сервер";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button SendJoke;
    }
}

