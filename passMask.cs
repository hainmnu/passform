using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

class Program{
	static void Main(string[] args){
		//ユーザ名、実行ファイルを指定しているか判定
		if(args.Length != 2){
			Environment.Exit(0);
		}

		Application.Run( new PassForm() );
	}
}

class PassForm:Form{
	//ボタン
	Button button_ok; //OKボタン
	Button button_cancel; //キャンセルボタン
	//ラベル
	Label label_caption;　//キャプション
	Label label_usr; //ユーザ名：
	Label label_pass; //パスワード：
	Label label_name; //取得したユーザ名
	//パネル
	Panel panel_pmask; //パスワード表示用
	//テキストボックス
	TextBox tb; //パスワード入力エリア

	public PassForm(){
		this.Width = 350;
		this.Height = 330;
		this.Text = "パスワード入力フォーム";
		this.FormBorderStyle = FormBorderStyle.FixedSingle;
		this.StartPosition = FormStartPosition.CenterScreen;
		this.AcceptButton = this.button_ok;

		//カレントディレクトリを取得
		string Dname = AppDomain.CurrentDomain.BaseDirectory;	

		//引数に指定したユーザ名を取得
		string[] cargs = Environment.GetCommandLineArgs();
		string Uname = cargs[1];

		//ラベル（キャプション）
		this.label_caption = new Label();
		this.label_caption.Size = new Size(200,30);
		this.label_caption.Location = new Point(this.Width / 2 - this.label_caption.Width / 2,this.label_caption.Height * 2);
		this.label_caption.Text = "パスワードを入力してください。";
		this.label_caption.TextAlign = ContentAlignment.MiddleCenter;
		this.Controls.Add(label_caption);

		//ラベル（ユーザ名：）
		this.label_usr = new Label();
		this.label_usr.Size = new Size(80,30);
		this.label_usr.Location = new Point(this.Width / 2 - this.label_usr.Width * 2, this.label_usr.Height * 3);
		this.label_usr.Text = "ユーザ名：";
		this.label_usr.TextAlign = ContentAlignment.MiddleLeft;
		this.Controls.Add(label_usr);

		//ラベル（パスワード：）
		this.label_pass = new Label();
		this.label_pass.Size = new Size(80,30);
		this.label_pass.Location = new Point(this.label_usr.Left, label_usr.Height * 4);
		this.label_pass.Text = "パスワード：";
		this.label_pass.TextAlign = ContentAlignment.MiddleLeft;
		this.Controls.Add(label_pass);


		//ラベル（取得するユーザ名）
		this.label_name = new Label();
		this.label_name.Size = new Size(100,30);
		this.label_name.Location = new Point(this.Width/2 - this.label_name.Width / 2, this.label_name.Height * 3);
		this.label_name.Text = Uname;
		this.label_name.TextAlign = ContentAlignment.MiddleLeft;
		this.Controls.Add(label_name);


		//テキストボックス（パスワード）
		this.tb = new TextBox();
		this.tb.Size = new Size(150,30);
		this.tb.Location = new Point(this.Width / 2 - this.tb.Width / 2, this.label_pass.Top + this.label_pass.Height / 4);
		this.tb.PasswordChar = '●';
		this.Controls.Add(tb);
		this.tb.KeyPress += new KeyPressEventHandler(this.tb_KeyPress);
		this.tb.KeyDown += new KeyEventHandler(this.tb_KeyDown);

		//パネル（パスワード表示用）
		this.panel_pmask = new Panel();
		this.panel_pmask.Size = new Size(30,30);
		this.panel_pmask.Location = new Point(this.tb.Right,this.tb.Top - this.panel_pmask.Height/4);
		this.panel_pmask.BackgroundImageLayout = ImageLayout.Zoom;
		this.panel_pmask.BackgroundImage = Image.FromFile(@Dname + "eye.png");
		this.panel_pmask.Visible = false;
		this.Controls.Add(panel_pmask);
		this.panel_pmask.MouseDown += new MouseEventHandler(this.panel_pmask_MouseDown);
		this.panel_pmask.MouseUp += new MouseEventHandler(this.panel_pmask_MouseUp);

		//ボタン（OKボタン）
		this.button_ok = new Button();
		this.button_ok.Size = new Size(80,30);
		this.button_ok.Location = new Point(this.Width - this.button_ok.Width / 2 * 5, this.Height - this.button_ok.Height * 3);
		this.button_ok.Text = "OK";
		this.button_ok.Click += new EventHandler(this.button_ok_click);
		this.Controls.Add(button_ok);

		//ボタン（キャンセル）
		this.button_cancel = new Button();
		this.button_cancel.Size = new Size(80,30);
		this.button_cancel.Location = new Point(this.Width - this.button_ok.Width / 2 * 5 + this.button_ok.Width +5, this.Height - this.button_ok.Height * 3);
		this.button_cancel.Text = "キャンセル";
		this.button_cancel.Click += new EventHandler(this.button_cancel_click);
		this.Controls.Add(button_cancel);
	}


	void tb_KeyPress(object sender, KeyPressEventArgs e){
		if( e.KeyChar == (char)Keys.Enter ){
			this.button_ok.PerformClick();	
		}

		if((e.KeyChar < 123 ) && (e.KeyChar != ((char)Keys.Back | (char)Keys.Enter))){
			this.panel_pmask.Visible = true;
		}

		if((e.KeyChar == (char)Keys.Back ) && this.tb.Text.Length == 1){
			this.panel_pmask.Visible = false;
		}

	}

	//Ctrl+Aで全選択
	void tb_KeyDown(object sender, KeyEventArgs e){
		if( e.KeyCode == Keys.A && e.Modifiers == Keys.Control ){
			this.tb.SelectAll();
		}
	}

	//マウスの左ボタンを押し続けているとき
	void panel_pmask_MouseDown(object sender, MouseEventArgs e){
		this.tb.Select(0,0);
		this.panel_pmask.BackColor = Color.LightGray;
		this.tb.PasswordChar = '\0';
	}

	//マウスの左ボタンから指を離した時
	void panel_pmask_MouseUp(object sender, MouseEventArgs e){
		this.tb.Select(this.tb.Text.Length,0);
		this.panel_pmask.BackColor = Color.Empty;
		this.tb.PasswordChar = '●';
	}

	//実行ファイルにユーザ名とパスワードを引数にして実行させる
	void button_ok_click(object sender, EventArgs e){
		string[] cargs = Environment.GetCommandLineArgs();
		string Fname = cargs[2];

		if(this.tb.Text.Length !=0 ){
			var psi = new ProcessStartInfo();
			psi.FileName = Fname;
			psi.Arguments = this.label_name.Text + " " + this.tb.Text;
			Process.Start(psi);

			this.Close();
		}
		else{
			MessageBox.Show("パスワードが入力されていません。");
		}
	}

	void button_cancel_click(object sender,EventArgs e){
		MessageBox.Show("キャンセルされました。");
		this.Close();	
	}

}
