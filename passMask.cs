using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

class Program{
	static void Main(string[] args){
		//���[�U���A���s�t�@�C�����w�肵�Ă��邩����
		if(args.Length != 2){
			Environment.Exit(0);
		}

		Application.Run( new PassForm() );
	}
}

class PassForm:Form{
	//�{�^��
	Button button_ok; //OK�{�^��
	Button button_cancel; //�L�����Z���{�^��
	//���x��
	Label label_caption;�@//�L���v�V����
	Label label_usr; //���[�U���F
	Label label_pass; //�p�X���[�h�F
	Label label_name; //�擾�������[�U��
	//�p�l��
	Panel panel_pmask; //�p�X���[�h�\���p
	//�e�L�X�g�{�b�N�X
	TextBox tb; //�p�X���[�h���̓G���A

	public PassForm(){
		this.Width = 350;
		this.Height = 330;
		this.Text = "�p�X���[�h���̓t�H�[��";
		this.FormBorderStyle = FormBorderStyle.FixedSingle;
		this.StartPosition = FormStartPosition.CenterScreen;
		this.AcceptButton = this.button_ok;

		//�J�����g�f�B���N�g�����擾
		string Dname = AppDomain.CurrentDomain.BaseDirectory;	

		//�����Ɏw�肵�����[�U�����擾
		string[] cargs = Environment.GetCommandLineArgs();
		string Uname = cargs[1];

		//���x���i�L���v�V�����j
		this.label_caption = new Label();
		this.label_caption.Size = new Size(200,30);
		this.label_caption.Location = new Point(this.Width / 2 - this.label_caption.Width / 2,this.label_caption.Height * 2);
		this.label_caption.Text = "�p�X���[�h����͂��Ă��������B";
		this.label_caption.TextAlign = ContentAlignment.MiddleCenter;
		this.Controls.Add(label_caption);

		//���x���i���[�U���F�j
		this.label_usr = new Label();
		this.label_usr.Size = new Size(80,30);
		this.label_usr.Location = new Point(this.Width / 2 - this.label_usr.Width * 2, this.label_usr.Height * 3);
		this.label_usr.Text = "���[�U���F";
		this.label_usr.TextAlign = ContentAlignment.MiddleLeft;
		this.Controls.Add(label_usr);

		//���x���i�p�X���[�h�F�j
		this.label_pass = new Label();
		this.label_pass.Size = new Size(80,30);
		this.label_pass.Location = new Point(this.label_usr.Left, label_usr.Height * 4);
		this.label_pass.Text = "�p�X���[�h�F";
		this.label_pass.TextAlign = ContentAlignment.MiddleLeft;
		this.Controls.Add(label_pass);


		//���x���i�擾���郆�[�U���j
		this.label_name = new Label();
		this.label_name.Size = new Size(100,30);
		this.label_name.Location = new Point(this.Width/2 - this.label_name.Width / 2, this.label_name.Height * 3);
		this.label_name.Text = Uname;
		this.label_name.TextAlign = ContentAlignment.MiddleLeft;
		this.Controls.Add(label_name);


		//�e�L�X�g�{�b�N�X�i�p�X���[�h�j
		this.tb = new TextBox();
		this.tb.Size = new Size(150,30);
		this.tb.Location = new Point(this.Width / 2 - this.tb.Width / 2, this.label_pass.Top + this.label_pass.Height / 4);
		this.tb.PasswordChar = '��';
		this.Controls.Add(tb);
		this.tb.KeyPress += new KeyPressEventHandler(this.tb_KeyPress);
		this.tb.KeyDown += new KeyEventHandler(this.tb_KeyDown);

		//�p�l���i�p�X���[�h�\���p�j
		this.panel_pmask = new Panel();
		this.panel_pmask.Size = new Size(30,30);
		this.panel_pmask.Location = new Point(this.tb.Right,this.tb.Top - this.panel_pmask.Height/4);
		this.panel_pmask.BackgroundImageLayout = ImageLayout.Zoom;
		this.panel_pmask.BackgroundImage = Image.FromFile(@Dname + "eye.png");
		this.panel_pmask.Visible = false;
		this.Controls.Add(panel_pmask);
		this.panel_pmask.MouseDown += new MouseEventHandler(this.panel_pmask_MouseDown);
		this.panel_pmask.MouseUp += new MouseEventHandler(this.panel_pmask_MouseUp);

		//�{�^���iOK�{�^���j
		this.button_ok = new Button();
		this.button_ok.Size = new Size(80,30);
		this.button_ok.Location = new Point(this.Width - this.button_ok.Width / 2 * 5, this.Height - this.button_ok.Height * 3);
		this.button_ok.Text = "OK";
		this.button_ok.Click += new EventHandler(this.button_ok_click);
		this.Controls.Add(button_ok);

		//�{�^���i�L�����Z���j
		this.button_cancel = new Button();
		this.button_cancel.Size = new Size(80,30);
		this.button_cancel.Location = new Point(this.Width - this.button_ok.Width / 2 * 5 + this.button_ok.Width +5, this.Height - this.button_ok.Height * 3);
		this.button_cancel.Text = "�L�����Z��";
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

	//Ctrl+A�őS�I��
	void tb_KeyDown(object sender, KeyEventArgs e){
		if( e.KeyCode == Keys.A && e.Modifiers == Keys.Control ){
			this.tb.SelectAll();
		}
	}

	//�}�E�X�̍��{�^�������������Ă���Ƃ�
	void panel_pmask_MouseDown(object sender, MouseEventArgs e){
		this.tb.Select(0,0);
		this.panel_pmask.BackColor = Color.LightGray;
		this.tb.PasswordChar = '\0';
	}

	//�}�E�X�̍��{�^������w�𗣂�����
	void panel_pmask_MouseUp(object sender, MouseEventArgs e){
		this.tb.Select(this.tb.Text.Length,0);
		this.panel_pmask.BackColor = Color.Empty;
		this.tb.PasswordChar = '��';
	}

	//���s�t�@�C���Ƀ��[�U���ƃp�X���[�h�������ɂ��Ď��s������
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
			MessageBox.Show("�p�X���[�h�����͂���Ă��܂���B");
		}
	}

	void button_cancel_click(object sender,EventArgs e){
		MessageBox.Show("�L�����Z������܂����B");
		this.Close();	
	}

}
