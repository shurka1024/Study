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
            this.On = new System.Windows.Forms.Button();
            this.Off = new System.Windows.Forms.Button();
            this.ConnectInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Joke = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SendJoke = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // On
            // 
            this.On.Location = new System.Drawing.Point(426, 35);
            this.On.Name = "On";
            this.On.Size = new System.Drawing.Size(75, 23);
            this.On.TabIndex = 0;
            this.On.Text = "Включить";
            this.On.UseVisualStyleBackColor = true;
            this.On.Click += new System.EventHandler(this.On_Click);
            // 
            // Off
            // 
            this.Off.Location = new System.Drawing.Point(426, 81);
            this.Off.Name = "Off";
            this.Off.Size = new System.Drawing.Size(75, 23);
            this.Off.TabIndex = 1;
            this.Off.Text = "Выключить";
            this.Off.UseVisualStyleBackColor = true;
            this.Off.Click += new System.EventHandler(this.Off_Click);
            // 
            // ConnectInfo
            // 
            this.ConnectInfo.Location = new System.Drawing.Point(12, 21);
            this.ConnectInfo.Multiline = true;
            this.ConnectInfo.Name = "ConnectInfo";
            this.ConnectInfo.ReadOnly = true;
            this.ConnectInfo.Size = new System.Drawing.Size(400, 172);
            this.ConnectInfo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Активные клиенты";
            // 
            // Joke
            // 
            this.Joke.Location = new System.Drawing.Point(12, 219);
            this.Joke.Name = "Joke";
            this.Joke.Size = new System.Drawing.Size(400, 20);
            this.Joke.TabIndex = 4;
            this.Joke.Text = "<ShowMustGoOn>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Введите текст-шутку:";
            // 
            // SendJoke
            // 
            this.SendJoke.Location = new System.Drawing.Point(426, 219);
            this.SendJoke.Name = "SendJoke";
            this.SendJoke.Size = new System.Drawing.Size(75, 23);
            this.SendJoke.TabIndex = 6;
            this.SendJoke.Text = "Отправить";
            this.SendJoke.UseVisualStyleBackColor = true;
            this.SendJoke.Click += new System.EventHandler(this.SendJoke_Click);
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 261);
            this.Controls.Add(this.SendJoke);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Joke);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConnectInfo);
            this.Controls.Add(this.Off);
            this.Controls.Add(this.On);
            this.Name = "FormClient";
            this.Text = "Сервер";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button On;
        private System.Windows.Forms.Button Off;
        private System.Windows.Forms.TextBox ConnectInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Joke;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SendJoke;
    }
}

