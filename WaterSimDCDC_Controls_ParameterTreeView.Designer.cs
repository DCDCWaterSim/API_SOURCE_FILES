namespace WaterSimDCDC.Controls
{
    partial class ParameterTreeView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParameterTreeView));
            this.treeViewParameters = new System.Windows.Forms.TreeView();
            this.contextMenuParameterTreeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFieldnamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFirstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byFieldnameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTreeNodes = new System.Windows.Forms.ImageList(this.components);
            this.statusStripTreeView = new System.Windows.Forms.StatusStrip();
            this.ItemStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelTreeViewKey = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelInputBaseKey = new System.Windows.Forms.Panel();
            this.labelInputBaseKey = new System.Windows.Forms.Label();
            this.picBoxInputBaseKey = new System.Windows.Forms.PictureBox();
            this.panelInputProviderKey = new System.Windows.Forms.Panel();
            this.labelInputProviderKey = new System.Windows.Forms.Label();
            this.picBoxInputProviderKey = new System.Windows.Forms.PictureBox();
            this.panelOutputBaseKey = new System.Windows.Forms.Panel();
            this.labelOutputBaseKey = new System.Windows.Forms.Label();
            this.picBoxOutputBaseKey = new System.Windows.Forms.PictureBox();
            this.panelOutputProviderKey = new System.Windows.Forms.Panel();
            this.labelOutputProviderKey = new System.Windows.Forms.Label();
            this.picBoxOutputProviderKey = new System.Windows.Forms.PictureBox();
            this.panelGroupKey = new System.Windows.Forms.Panel();
            this.labelGroupKey = new System.Windows.Forms.Label();
            this.picBoxGroupKey = new System.Windows.Forms.PictureBox();
            this.panelNotActiveKey = new System.Windows.Forms.Panel();
            this.labelNotActiveKey = new System.Windows.Forms.Label();
            this.picBoxNotActiveKey = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuParameterTreeview.SuspendLayout();
            this.statusStripTreeView.SuspendLayout();
            this.panelTreeViewKey.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelInputBaseKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxInputBaseKey)).BeginInit();
            this.panelInputProviderKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxInputProviderKey)).BeginInit();
            this.panelOutputBaseKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOutputBaseKey)).BeginInit();
            this.panelOutputProviderKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOutputProviderKey)).BeginInit();
            this.panelGroupKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGroupKey)).BeginInit();
            this.panelNotActiveKey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxNotActiveKey)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewParameters
            // 
            this.treeViewParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewParameters.ContextMenuStrip = this.contextMenuParameterTreeview;
            this.treeViewParameters.Location = new System.Drawing.Point(3, 0);
            this.treeViewParameters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeViewParameters.Name = "treeViewParameters";
            this.treeViewParameters.Size = new System.Drawing.Size(453, 530);
            this.treeViewParameters.TabIndex = 0;
            this.treeViewParameters.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewParameters_BeforeCheck);
            this.treeViewParameters.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewParameters_AfterCheck);
            this.treeViewParameters.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewParameters_BeforeSelect);
            this.treeViewParameters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewParameters_AfterSelect);
            this.treeViewParameters.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewParameters_NodeMouseClick);
            this.treeViewParameters.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewParameters_NodeMouseDoubleClick);
            this.treeViewParameters.DoubleClick += new System.EventHandler(this.treeViewParameters_DoubleClick);
            this.treeViewParameters.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeViewParameters_MouseClick);
            this.treeViewParameters.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeViewParameters_MouseDoubleClick);
            // 
            // contextMenuParameterTreeview
            // 
            this.contextMenuParameterTreeview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem,
            this.sortToolStripMenuItem,
            this.treeToolStripMenuItem});
            this.contextMenuParameterTreeview.Name = "contextMenuParameterTreeview";
            this.contextMenuParameterTreeview.Size = new System.Drawing.Size(128, 76);
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideKeyToolStripMenuItem,
            this.showFieldnamesToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // hideKeyToolStripMenuItem
            // 
            this.hideKeyToolStripMenuItem.Name = "hideKeyToolStripMenuItem";
            this.hideKeyToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.hideKeyToolStripMenuItem.Text = "Hide Key";
            this.hideKeyToolStripMenuItem.Click += new System.EventHandler(this.hideKeyToolStripMenuItem_Click);
            // 
            // showFieldnamesToolStripMenuItem
            // 
            this.showFieldnamesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showFirstToolStripMenuItem,
            this.showLastToolStripMenuItem,
            this.hideToolStripMenuItem});
            this.showFieldnamesToolStripMenuItem.Name = "showFieldnamesToolStripMenuItem";
            this.showFieldnamesToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.showFieldnamesToolStripMenuItem.Text = "Show Fieldnames";
            // 
            // showFirstToolStripMenuItem
            // 
            this.showFirstToolStripMenuItem.CheckOnClick = true;
            this.showFirstToolStripMenuItem.Name = "showFirstToolStripMenuItem";
            this.showFirstToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.showFirstToolStripMenuItem.Text = "Show First";
            this.showFirstToolStripMenuItem.CheckedChanged += new System.EventHandler(this.showFirstToolStripMenuItem_CheckedChanged);
            // 
            // showLastToolStripMenuItem
            // 
            this.showLastToolStripMenuItem.CheckOnClick = true;
            this.showLastToolStripMenuItem.Name = "showLastToolStripMenuItem";
            this.showLastToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.showLastToolStripMenuItem.Text = "Show Last";
            this.showLastToolStripMenuItem.CheckedChanged += new System.EventHandler(this.showLastToolStripMenuItem_CheckedChanged);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.CheckOnClick = true;
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.CheckedChanged += new System.EventHandler(this.hideToolStripMenuItem_CheckedChanged);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byLabelToolStripMenuItem,
            this.byFieldnameToolStripMenuItem});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // byLabelToolStripMenuItem
            // 
            this.byLabelToolStripMenuItem.Name = "byLabelToolStripMenuItem";
            this.byLabelToolStripMenuItem.Size = new System.Drawing.Size(167, 24);
            this.byLabelToolStripMenuItem.Text = "By Label";
            this.byLabelToolStripMenuItem.Click += new System.EventHandler(this.byLabelToolStripMenuItem_Click);
            // 
            // byFieldnameToolStripMenuItem
            // 
            this.byFieldnameToolStripMenuItem.Name = "byFieldnameToolStripMenuItem";
            this.byFieldnameToolStripMenuItem.Size = new System.Drawing.Size(167, 24);
            this.byFieldnameToolStripMenuItem.Text = "By Fieldname";
            this.byFieldnameToolStripMenuItem.Click += new System.EventHandler(this.byFieldnameToolStripMenuItem_Click);
            // 
            // treeToolStripMenuItem
            // 
            this.treeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandAllToolStripMenuItem,
            this.collapseAllToolStripMenuItem});
            this.treeToolStripMenuItem.Name = "treeToolStripMenuItem";
            this.treeToolStripMenuItem.Size = new System.Drawing.Size(127, 24);
            this.treeToolStripMenuItem.Text = "Tree";
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            // 
            // imageListTreeNodes
            // 
            this.imageListTreeNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeNodes.ImageStream")));
            this.imageListTreeNodes.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeNodes.Images.SetKeyName(0, "green arrow left.png");
            this.imageListTreeNodes.Images.SetKeyName(1, "green round arrow going right 32 x 32.png");
            this.imageListTreeNodes.Images.SetKeyName(2, "red arrow left.png");
            this.imageListTreeNodes.Images.SetKeyName(3, "red round arroe going left 32 x 32psd.png");
            this.imageListTreeNodes.Images.SetKeyName(4, "blue button double arrow.png");
            this.imageListTreeNodes.Images.SetKeyName(5, "blue circle with arrow.png");
            this.imageListTreeNodes.Images.SetKeyName(6, "red round question 32 x 32.png");
            this.imageListTreeNodes.Images.SetKeyName(7, "grey arrow right 32 x 32.png");
            this.imageListTreeNodes.Images.SetKeyName(8, "Grey Circle with Arrow Right 32 x 32.png");
            this.imageListTreeNodes.Images.SetKeyName(9, "grey arrow left 32 x 32.png");
            this.imageListTreeNodes.Images.SetKeyName(10, "grey circle arrow left 32 x 32.png");
            this.imageListTreeNodes.Images.SetKeyName(11, "grey buttondouble arrow left right  32 x 32.png");
            this.imageListTreeNodes.Images.SetKeyName(12, "Grey circle with arrow 32 x 32 4 .png");
            this.imageListTreeNodes.Images.SetKeyName(13, "grey  round question 32 x 32.png");
            // 
            // statusStripTreeView
            // 
            this.statusStripTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemStatusLabel});
            this.statusStripTreeView.Location = new System.Drawing.Point(0, 648);
            this.statusStripTreeView.Name = "statusStripTreeView";
            this.statusStripTreeView.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStripTreeView.Size = new System.Drawing.Size(459, 22);
            this.statusStripTreeView.TabIndex = 1;
            this.statusStripTreeView.Text = "statusStrip1";
            // 
            // ItemStatusLabel
            // 
            this.ItemStatusLabel.Name = "ItemStatusLabel";
            this.ItemStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // panelTreeViewKey
            // 
            this.panelTreeViewKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTreeViewKey.AutoScroll = true;
            this.panelTreeViewKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTreeViewKey.Controls.Add(this.flowLayoutPanel1);
            this.panelTreeViewKey.Controls.Add(this.label1);
            this.panelTreeViewKey.Location = new System.Drawing.Point(3, 535);
            this.panelTreeViewKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelTreeViewKey.Name = "panelTreeViewKey";
            this.panelTreeViewKey.Size = new System.Drawing.Size(456, 105);
            this.panelTreeViewKey.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.panelInputBaseKey);
            this.flowLayoutPanel1.Controls.Add(this.panelInputProviderKey);
            this.flowLayoutPanel1.Controls.Add(this.panelOutputBaseKey);
            this.flowLayoutPanel1.Controls.Add(this.panelOutputProviderKey);
            this.flowLayoutPanel1.Controls.Add(this.panelGroupKey);
            this.flowLayoutPanel1.Controls.Add(this.panelNotActiveKey);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(56, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(333, 95);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // panelInputBaseKey
            // 
            this.panelInputBaseKey.AutoSize = true;
            this.panelInputBaseKey.Controls.Add(this.labelInputBaseKey);
            this.panelInputBaseKey.Controls.Add(this.picBoxInputBaseKey);
            this.panelInputBaseKey.Location = new System.Drawing.Point(3, 2);
            this.panelInputBaseKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelInputBaseKey.Name = "panelInputBaseKey";
            this.panelInputBaseKey.Size = new System.Drawing.Size(115, 26);
            this.panelInputBaseKey.TabIndex = 0;
            // 
            // labelInputBaseKey
            // 
            this.labelInputBaseKey.AutoSize = true;
            this.labelInputBaseKey.Location = new System.Drawing.Point(37, 9);
            this.labelInputBaseKey.Name = "labelInputBaseKey";
            this.labelInputBaseKey.Size = new System.Drawing.Size(75, 17);
            this.labelInputBaseKey.TabIndex = 1;
            this.labelInputBaseKey.Text = "Input Base";
            // 
            // picBoxInputBaseKey
            // 
            this.picBoxInputBaseKey.Image = global::WaterSimDCDC_Visual_Controls.Resource_Visual.green_arrow_left;
            this.picBoxInputBaseKey.Location = new System.Drawing.Point(8, 6);
            this.picBoxInputBaseKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picBoxInputBaseKey.Name = "picBoxInputBaseKey";
            this.picBoxInputBaseKey.Size = new System.Drawing.Size(16, 16);
            this.picBoxInputBaseKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxInputBaseKey.TabIndex = 0;
            this.picBoxInputBaseKey.TabStop = false;
            // 
            // panelInputProviderKey
            // 
            this.panelInputProviderKey.AutoSize = true;
            this.panelInputProviderKey.Controls.Add(this.labelInputProviderKey);
            this.panelInputProviderKey.Controls.Add(this.picBoxInputProviderKey);
            this.panelInputProviderKey.Location = new System.Drawing.Point(124, 2);
            this.panelInputProviderKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelInputProviderKey.Name = "panelInputProviderKey";
            this.panelInputProviderKey.Size = new System.Drawing.Size(136, 26);
            this.panelInputProviderKey.TabIndex = 1;
            // 
            // labelInputProviderKey
            // 
            this.labelInputProviderKey.AutoSize = true;
            this.labelInputProviderKey.Location = new System.Drawing.Point(37, 9);
            this.labelInputProviderKey.Name = "labelInputProviderKey";
            this.labelInputProviderKey.Size = new System.Drawing.Size(96, 17);
            this.labelInputProviderKey.TabIndex = 1;
            this.labelInputProviderKey.Text = "Input Provider";
            // 
            // picBoxInputProviderKey
            // 
            this.picBoxInputProviderKey.Image = global::WaterSimDCDC_Visual_Controls.Resource_Visual.green_round_arrow_going_right_32_x_32;
            this.picBoxInputProviderKey.Location = new System.Drawing.Point(8, 6);
            this.picBoxInputProviderKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picBoxInputProviderKey.Name = "picBoxInputProviderKey";
            this.picBoxInputProviderKey.Size = new System.Drawing.Size(16, 16);
            this.picBoxInputProviderKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxInputProviderKey.TabIndex = 0;
            this.picBoxInputProviderKey.TabStop = false;
            // 
            // panelOutputBaseKey
            // 
            this.panelOutputBaseKey.AutoSize = true;
            this.panelOutputBaseKey.Controls.Add(this.labelOutputBaseKey);
            this.panelOutputBaseKey.Controls.Add(this.picBoxOutputBaseKey);
            this.panelOutputBaseKey.Location = new System.Drawing.Point(3, 32);
            this.panelOutputBaseKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelOutputBaseKey.Name = "panelOutputBaseKey";
            this.panelOutputBaseKey.Size = new System.Drawing.Size(127, 24);
            this.panelOutputBaseKey.TabIndex = 2;
            // 
            // labelOutputBaseKey
            // 
            this.labelOutputBaseKey.AutoSize = true;
            this.labelOutputBaseKey.Location = new System.Drawing.Point(37, 7);
            this.labelOutputBaseKey.Name = "labelOutputBaseKey";
            this.labelOutputBaseKey.Size = new System.Drawing.Size(87, 17);
            this.labelOutputBaseKey.TabIndex = 1;
            this.labelOutputBaseKey.Text = "Output Base";
            // 
            // picBoxOutputBaseKey
            // 
            this.picBoxOutputBaseKey.Image = global::WaterSimDCDC_Visual_Controls.Resource_Visual.red_arrow_left;
            this.picBoxOutputBaseKey.Location = new System.Drawing.Point(8, 6);
            this.picBoxOutputBaseKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picBoxOutputBaseKey.Name = "picBoxOutputBaseKey";
            this.picBoxOutputBaseKey.Size = new System.Drawing.Size(16, 16);
            this.picBoxOutputBaseKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxOutputBaseKey.TabIndex = 0;
            this.picBoxOutputBaseKey.TabStop = false;
            // 
            // panelOutputProviderKey
            // 
            this.panelOutputProviderKey.AutoSize = true;
            this.panelOutputProviderKey.Controls.Add(this.labelOutputProviderKey);
            this.panelOutputProviderKey.Controls.Add(this.picBoxOutputProviderKey);
            this.panelOutputProviderKey.Location = new System.Drawing.Point(136, 32);
            this.panelOutputProviderKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelOutputProviderKey.Name = "panelOutputProviderKey";
            this.panelOutputProviderKey.Size = new System.Drawing.Size(148, 26);
            this.panelOutputProviderKey.TabIndex = 3;
            // 
            // labelOutputProviderKey
            // 
            this.labelOutputProviderKey.AutoSize = true;
            this.labelOutputProviderKey.Location = new System.Drawing.Point(37, 9);
            this.labelOutputProviderKey.Name = "labelOutputProviderKey";
            this.labelOutputProviderKey.Size = new System.Drawing.Size(108, 17);
            this.labelOutputProviderKey.TabIndex = 1;
            this.labelOutputProviderKey.Text = "Output Provider";
            // 
            // picBoxOutputProviderKey
            // 
            this.picBoxOutputProviderKey.Image = global::WaterSimDCDC_Visual_Controls.Resource_Visual.red_round_arroe_going_left_32_x_32psd;
            this.picBoxOutputProviderKey.Location = new System.Drawing.Point(8, 6);
            this.picBoxOutputProviderKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picBoxOutputProviderKey.Name = "picBoxOutputProviderKey";
            this.picBoxOutputProviderKey.Size = new System.Drawing.Size(16, 16);
            this.picBoxOutputProviderKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxOutputProviderKey.TabIndex = 0;
            this.picBoxOutputProviderKey.TabStop = false;
            // 
            // panelGroupKey
            // 
            this.panelGroupKey.AutoSize = true;
            this.panelGroupKey.Controls.Add(this.labelGroupKey);
            this.panelGroupKey.Controls.Add(this.picBoxGroupKey);
            this.panelGroupKey.Location = new System.Drawing.Point(3, 62);
            this.panelGroupKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelGroupKey.Name = "panelGroupKey";
            this.panelGroupKey.Size = new System.Drawing.Size(127, 26);
            this.panelGroupKey.TabIndex = 4;
            // 
            // labelGroupKey
            // 
            this.labelGroupKey.AutoSize = true;
            this.labelGroupKey.Location = new System.Drawing.Point(37, 9);
            this.labelGroupKey.Name = "labelGroupKey";
            this.labelGroupKey.Size = new System.Drawing.Size(87, 17);
            this.labelGroupKey.TabIndex = 1;
            this.labelGroupKey.Text = "Topic Group";
            // 
            // picBoxGroupKey
            // 
            this.picBoxGroupKey.Image = global::WaterSimDCDC_Visual_Controls.Resource_Visual.blue_buttondouble_arrow_left_right__32_x_32;
            this.picBoxGroupKey.Location = new System.Drawing.Point(8, 6);
            this.picBoxGroupKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picBoxGroupKey.Name = "picBoxGroupKey";
            this.picBoxGroupKey.Size = new System.Drawing.Size(16, 16);
            this.picBoxGroupKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxGroupKey.TabIndex = 0;
            this.picBoxGroupKey.TabStop = false;
            // 
            // panelNotActiveKey
            // 
            this.panelNotActiveKey.AutoSize = true;
            this.panelNotActiveKey.Controls.Add(this.labelNotActiveKey);
            this.panelNotActiveKey.Controls.Add(this.picBoxNotActiveKey);
            this.panelNotActiveKey.Location = new System.Drawing.Point(136, 62);
            this.panelNotActiveKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelNotActiveKey.Name = "panelNotActiveKey";
            this.panelNotActiveKey.Size = new System.Drawing.Size(112, 26);
            this.panelNotActiveKey.TabIndex = 5;
            // 
            // labelNotActiveKey
            // 
            this.labelNotActiveKey.AutoSize = true;
            this.labelNotActiveKey.Location = new System.Drawing.Point(37, 9);
            this.labelNotActiveKey.Name = "labelNotActiveKey";
            this.labelNotActiveKey.Size = new System.Drawing.Size(72, 17);
            this.labelNotActiveKey.TabIndex = 1;
            this.labelNotActiveKey.Text = "Not Active";
            // 
            // picBoxNotActiveKey
            // 
            this.picBoxNotActiveKey.Image = global::WaterSimDCDC_Visual_Controls.Resource_Visual.red_round_question_32_x_32;
            this.picBoxNotActiveKey.Location = new System.Drawing.Point(8, 6);
            this.picBoxNotActiveKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picBoxNotActiveKey.Name = "picBoxNotActiveKey";
            this.picBoxNotActiveKey.Size = new System.Drawing.Size(16, 16);
            this.picBoxNotActiveKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxNotActiveKey.TabIndex = 0;
            this.picBoxNotActiveKey.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key:";
            // 
            // ParameterTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelTreeViewKey);
            this.Controls.Add(this.statusStripTreeView);
            this.Controls.Add(this.treeViewParameters);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ParameterTreeView";
            this.Size = new System.Drawing.Size(459, 670);
            this.contextMenuParameterTreeview.ResumeLayout(false);
            this.statusStripTreeView.ResumeLayout(false);
            this.statusStripTreeView.PerformLayout();
            this.panelTreeViewKey.ResumeLayout(false);
            this.panelTreeViewKey.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panelInputBaseKey.ResumeLayout(false);
            this.panelInputBaseKey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxInputBaseKey)).EndInit();
            this.panelInputProviderKey.ResumeLayout(false);
            this.panelInputProviderKey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxInputProviderKey)).EndInit();
            this.panelOutputBaseKey.ResumeLayout(false);
            this.panelOutputBaseKey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOutputBaseKey)).EndInit();
            this.panelOutputProviderKey.ResumeLayout(false);
            this.panelOutputProviderKey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxOutputProviderKey)).EndInit();
            this.panelGroupKey.ResumeLayout(false);
            this.panelGroupKey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGroupKey)).EndInit();
            this.panelNotActiveKey.ResumeLayout(false);
            this.panelNotActiveKey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxNotActiveKey)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewParameters;
        private System.Windows.Forms.ImageList imageListTreeNodes;
        private System.Windows.Forms.StatusStrip statusStripTreeView;
        private System.Windows.Forms.Panel panelTreeViewKey;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelInputBaseKey;
        private System.Windows.Forms.Label labelInputBaseKey;
        private System.Windows.Forms.PictureBox picBoxInputBaseKey;
        private System.Windows.Forms.Panel panelInputProviderKey;
        private System.Windows.Forms.Label labelInputProviderKey;
        private System.Windows.Forms.PictureBox picBoxInputProviderKey;
        private System.Windows.Forms.Panel panelOutputBaseKey;
        private System.Windows.Forms.Label labelOutputBaseKey;
        private System.Windows.Forms.PictureBox picBoxOutputBaseKey;
        private System.Windows.Forms.Panel panelOutputProviderKey;
        private System.Windows.Forms.Label labelOutputProviderKey;
        private System.Windows.Forms.PictureBox picBoxOutputProviderKey;
        private System.Windows.Forms.Panel panelGroupKey;
        private System.Windows.Forms.Label labelGroupKey;
        private System.Windows.Forms.PictureBox picBoxGroupKey;
        private System.Windows.Forms.Panel panelNotActiveKey;
        private System.Windows.Forms.Label labelNotActiveKey;
        private System.Windows.Forms.PictureBox picBoxNotActiveKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuParameterTreeview;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFieldnamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFirstToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byFieldnameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem treeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel ItemStatusLabel;
    }
}
