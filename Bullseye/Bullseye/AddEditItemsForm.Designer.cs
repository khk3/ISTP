namespace Bullseye
{
    partial class AddEditItemsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditItemsForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.ckbActive = new System.Windows.Forms.CheckBox();
            this.txtItemID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSku = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.txtCostPrice = new System.Windows.Forms.TextBox();
            this.txtRetailPrice = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.RichTextBox();
            this.btnResetItem = new System.Windows.Forms.Button();
            this.btnSaveItem = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.warnItemID = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.cboSuppliers = new System.Windows.Forms.ComboBox();
            this.txtCaseSize = new System.Windows.Forms.TextBox();
            this.picImgEditItem = new System.Windows.Forms.PictureBox();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.warnSku = new System.Windows.Forms.Label();
            this.warnName = new System.Windows.Forms.Label();
            this.warnDesc = new System.Windows.Forms.Label();
            this.warnCat = new System.Windows.Forms.Label();
            this.warnRetail = new System.Windows.Forms.Label();
            this.warnCost = new System.Windows.Forms.Label();
            this.warnCase = new System.Windows.Forms.Label();
            this.warnWeight = new System.Windows.Forms.Label();
            this.warnSupp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImgEditItem)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Handwriting", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(324, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bullseye";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(249, 46);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(69, 57);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(126, 384);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "ItemID: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(131, 417);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Name: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(356, 384);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "SKU: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(89, 442);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 20);
            this.label9.TabIndex = 9;
            this.label9.Text = "Description: ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(99, 475);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 20);
            this.label12.TabIndex = 12;
            this.label12.Text = "Category:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(118, 534);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 20);
            this.label13.TabIndex = 13;
            this.label13.Text = "Weight: ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(89, 506);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 20);
            this.label14.TabIndex = 14;
            this.label14.Text = "Case Size: ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(384, 501);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 20);
            this.label15.TabIndex = 15;
            this.label15.Text = "Cost Price: ";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(378, 475);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(106, 20);
            this.label16.TabIndex = 16;
            this.label16.Text = "Retail Price: ";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(384, 531);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(102, 20);
            this.label17.TabIndex = 17;
            this.label17.Text = "SupplierID:  ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(112, 573);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 20);
            this.label19.TabIndex = 19;
            this.label19.Text = "Notes:  ";
            // 
            // ckbActive
            // 
            this.ckbActive.AutoSize = true;
            this.ckbActive.Location = new System.Drawing.Point(561, 382);
            this.ckbActive.Name = "ckbActive";
            this.ckbActive.Size = new System.Drawing.Size(66, 20);
            this.ckbActive.TabIndex = 2;
            this.ckbActive.Text = "Active";
            this.ckbActive.UseVisualStyleBackColor = true;
            // 
            // txtItemID
            // 
            this.txtItemID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemID.Enabled = false;
            this.txtItemID.Location = new System.Drawing.Point(195, 384);
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.Size = new System.Drawing.Size(123, 22);
            this.txtItemID.TabIndex = 0;
            this.txtItemID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtItemID.TextChanged += new System.EventHandler(this.txtItemID_TextChanged);
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(195, 412);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(432, 22);
            this.txtName.TabIndex = 3;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtSku
            // 
            this.txtSku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSku.Location = new System.Drawing.Point(404, 382);
            this.txtSku.Name = "txtSku";
            this.txtSku.Size = new System.Drawing.Size(123, 22);
            this.txtSku.TabIndex = 1;
            this.txtSku.TextChanged += new System.EventHandler(this.txtSku_TextChanged);
            this.txtSku.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSku_KeyPress);
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(195, 440);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(432, 22);
            this.txtDescription.TabIndex = 4;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(195, 471);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(159, 24);
            this.cboCategory.TabIndex = 5;
            // 
            // txtWeight
            // 
            this.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWeight.Location = new System.Drawing.Point(195, 534);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(93, 22);
            this.txtWeight.TabIndex = 9;
            this.txtWeight.TextChanged += new System.EventHandler(this.txtWeight_TextChanged);
            this.txtWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWeight_KeyPress);
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCostPrice.Location = new System.Drawing.Point(477, 499);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(93, 22);
            this.txtCostPrice.TabIndex = 8;
            this.txtCostPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCostPrice_KeyPress);
            // 
            // txtRetailPrice
            // 
            this.txtRetailPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRetailPrice.Location = new System.Drawing.Point(477, 471);
            this.txtRetailPrice.Name = "txtRetailPrice";
            this.txtRetailPrice.Size = new System.Drawing.Size(93, 22);
            this.txtRetailPrice.TabIndex = 6;
            this.txtRetailPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRetailPrice_KeyPress);
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(195, 573);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(447, 96);
            this.txtNotes.TabIndex = 11;
            this.txtNotes.Text = "";
            this.txtNotes.TextChanged += new System.EventHandler(this.txtNotes_TextChanged);
            // 
            // btnResetItem
            // 
            this.btnResetItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetItem.Location = new System.Drawing.Point(360, 696);
            this.btnResetItem.Name = "btnResetItem";
            this.btnResetItem.Size = new System.Drawing.Size(103, 40);
            this.btnResetItem.TabIndex = 13;
            this.btnResetItem.Text = "&Reset";
            this.btnResetItem.UseVisualStyleBackColor = true;
            this.btnResetItem.Click += new System.EventHandler(this.btnResetItem_Click);
            // 
            // btnSaveItem
            // 
            this.btnSaveItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveItem.Location = new System.Drawing.Point(539, 696);
            this.btnSaveItem.Name = "btnSaveItem";
            this.btnSaveItem.Size = new System.Drawing.Size(103, 40);
            this.btnSaveItem.TabIndex = 14;
            this.btnSaveItem.Text = "&Save ";
            this.btnSaveItem.UseVisualStyleBackColor = true;
            this.btnSaveItem.Click += new System.EventHandler(this.btnSaveItem_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(195, 696);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(103, 40);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // warnItemID
            // 
            this.warnItemID.AutoSize = true;
            this.warnItemID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnItemID.ForeColor = System.Drawing.Color.Red;
            this.warnItemID.Location = new System.Drawing.Point(324, 386);
            this.warnItemID.Name = "warnItemID";
            this.warnItemID.Size = new System.Drawing.Size(11, 16);
            this.warnItemID.TabIndex = 35;
            this.warnItemID.Text = "!";
            this.warnItemID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warnItemID.Visible = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(576, 532);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(12, 16);
            this.label24.TabIndex = 42;
            this.label24.Text = "*";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboSuppliers
            // 
            this.cboSuppliers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSuppliers.FormattingEnabled = true;
            this.cboSuppliers.Location = new System.Drawing.Point(483, 527);
            this.cboSuppliers.Name = "cboSuppliers";
            this.cboSuppliers.Size = new System.Drawing.Size(159, 24);
            this.cboSuppliers.TabIndex = 10;
            // 
            // txtCaseSize
            // 
            this.txtCaseSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCaseSize.Location = new System.Drawing.Point(195, 506);
            this.txtCaseSize.Name = "txtCaseSize";
            this.txtCaseSize.Size = new System.Drawing.Size(93, 22);
            this.txtCaseSize.TabIndex = 7;
            this.txtCaseSize.TextChanged += new System.EventHandler(this.txtCaseSize_TextChanged);
            this.txtCaseSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCaseSize_KeyPress);
            // 
            // picImgEditItem
            // 
            this.picImgEditItem.Image = ((System.Drawing.Image)(resources.GetObject("picImgEditItem.Image")));
            this.picImgEditItem.Location = new System.Drawing.Point(334, 141);
            this.picImgEditItem.Name = "picImgEditItem";
            this.picImgEditItem.Size = new System.Drawing.Size(205, 191);
            this.picImgEditItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImgEditItem.TabIndex = 46;
            this.picImgEditItem.TabStop = false;
            // 
            // btnAddImage
            // 
            this.btnAddImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddImage.Location = new System.Drawing.Point(561, 229);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(151, 29);
            this.btnAddImage.TabIndex = 47;
            this.btnAddImage.Text = "&Add/Change Image";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // warnSku
            // 
            this.warnSku.AutoSize = true;
            this.warnSku.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnSku.ForeColor = System.Drawing.Color.Red;
            this.warnSku.Location = new System.Drawing.Point(533, 384);
            this.warnSku.Name = "warnSku";
            this.warnSku.Size = new System.Drawing.Size(11, 16);
            this.warnSku.TabIndex = 48;
            this.warnSku.Text = "!";
            this.warnSku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warnSku.Visible = false;
            // 
            // warnName
            // 
            this.warnName.AutoSize = true;
            this.warnName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnName.ForeColor = System.Drawing.Color.Red;
            this.warnName.Location = new System.Drawing.Point(631, 412);
            this.warnName.Name = "warnName";
            this.warnName.Size = new System.Drawing.Size(11, 16);
            this.warnName.TabIndex = 49;
            this.warnName.Text = "!";
            this.warnName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warnName.Visible = false;
            // 
            // warnDesc
            // 
            this.warnDesc.AutoSize = true;
            this.warnDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnDesc.ForeColor = System.Drawing.Color.Red;
            this.warnDesc.Location = new System.Drawing.Point(631, 442);
            this.warnDesc.Name = "warnDesc";
            this.warnDesc.Size = new System.Drawing.Size(11, 16);
            this.warnDesc.TabIndex = 50;
            this.warnDesc.Text = "!";
            this.warnDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warnDesc.Visible = false;
            // 
            // warnCat
            // 
            this.warnCat.AutoSize = true;
            this.warnCat.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnCat.ForeColor = System.Drawing.Color.Red;
            this.warnCat.Location = new System.Drawing.Point(357, 475);
            this.warnCat.Name = "warnCat";
            this.warnCat.Size = new System.Drawing.Size(11, 16);
            this.warnCat.TabIndex = 51;
            this.warnCat.Text = "!";
            this.warnCat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warnCat.Visible = false;
            // 
            // warnRetail
            // 
            this.warnRetail.AutoSize = true;
            this.warnRetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnRetail.ForeColor = System.Drawing.Color.Red;
            this.warnRetail.Location = new System.Drawing.Point(576, 471);
            this.warnRetail.Name = "warnRetail";
            this.warnRetail.Size = new System.Drawing.Size(11, 16);
            this.warnRetail.TabIndex = 52;
            this.warnRetail.Text = "!";
            this.warnRetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warnRetail.Visible = false;
            // 
            // warnCost
            // 
            this.warnCost.AutoSize = true;
            this.warnCost.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnCost.ForeColor = System.Drawing.Color.Red;
            this.warnCost.Location = new System.Drawing.Point(576, 501);
            this.warnCost.Name = "warnCost";
            this.warnCost.Size = new System.Drawing.Size(11, 16);
            this.warnCost.TabIndex = 53;
            this.warnCost.Text = "!";
            this.warnCost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warnCost.Visible = false;
            // 
            // warnCase
            // 
            this.warnCase.AutoSize = true;
            this.warnCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnCase.ForeColor = System.Drawing.Color.Red;
            this.warnCase.Location = new System.Drawing.Point(294, 506);
            this.warnCase.Name = "warnCase";
            this.warnCase.Size = new System.Drawing.Size(11, 16);
            this.warnCase.TabIndex = 54;
            this.warnCase.Text = "!";
            this.warnCase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warnCase.Visible = false;
            // 
            // warnWeight
            // 
            this.warnWeight.AutoSize = true;
            this.warnWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnWeight.ForeColor = System.Drawing.Color.Red;
            this.warnWeight.Location = new System.Drawing.Point(294, 536);
            this.warnWeight.Name = "warnWeight";
            this.warnWeight.Size = new System.Drawing.Size(11, 16);
            this.warnWeight.TabIndex = 55;
            this.warnWeight.Text = "!";
            this.warnWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warnWeight.Visible = false;
            // 
            // warnSupp
            // 
            this.warnSupp.AutoSize = true;
            this.warnSupp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnSupp.ForeColor = System.Drawing.Color.Red;
            this.warnSupp.Location = new System.Drawing.Point(648, 530);
            this.warnSupp.Name = "warnSupp";
            this.warnSupp.Size = new System.Drawing.Size(11, 16);
            this.warnSupp.TabIndex = 56;
            this.warnSupp.Text = "!";
            this.warnSupp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.warnSupp.Visible = false;
            // 
            // AddEditItemsForm
            // 
            this.AcceptButton = this.btnSaveItem;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(824, 794);
            this.Controls.Add(this.warnSupp);
            this.Controls.Add(this.warnWeight);
            this.Controls.Add(this.warnCase);
            this.Controls.Add(this.warnCost);
            this.Controls.Add(this.warnRetail);
            this.Controls.Add(this.warnCat);
            this.Controls.Add(this.warnDesc);
            this.Controls.Add(this.warnName);
            this.Controls.Add(this.warnSku);
            this.Controls.Add(this.btnAddImage);
            this.Controls.Add(this.picImgEditItem);
            this.Controls.Add(this.cboSuppliers);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.warnItemID);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSaveItem);
            this.Controls.Add(this.btnResetItem);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtRetailPrice);
            this.Controls.Add(this.txtCostPrice);
            this.Controls.Add(this.txtCaseSize);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtSku);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtItemID);
            this.Controls.Add(this.ckbActive);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AddEditItemsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bullseye ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddEditItemsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImgEditItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox ckbActive;
        private System.Windows.Forms.TextBox txtItemID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSku;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.TextBox txtCostPrice;
        private System.Windows.Forms.TextBox txtRetailPrice;
        private System.Windows.Forms.RichTextBox txtNotes;
        private System.Windows.Forms.Button btnResetItem;
        private System.Windows.Forms.Button btnSaveItem;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label warnItemID;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cboSuppliers;
        private System.Windows.Forms.TextBox txtCaseSize;
        private System.Windows.Forms.PictureBox picImgEditItem;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.Label warnSku;
        private System.Windows.Forms.Label warnName;
        private System.Windows.Forms.Label warnDesc;
        private System.Windows.Forms.Label warnCat;
        private System.Windows.Forms.Label warnRetail;
        private System.Windows.Forms.Label warnCost;
        private System.Windows.Forms.Label warnCase;
        private System.Windows.Forms.Label warnWeight;
        private System.Windows.Forms.Label warnSupp;
    }
}