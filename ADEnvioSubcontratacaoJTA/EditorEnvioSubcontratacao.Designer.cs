namespace ADEnvioSubcontratacaoJTA
{
    partial class EditorEnvioSubcontratacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorEnvioSubcontratacao));
            this.f41 = new PRISDK100.F4();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CheckItem = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.OrdemFabrico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operacao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operacaoDescricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Artigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArmazemDest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoDest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Servico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescricaoServico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unidade2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtfOF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtdSubc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtdaSubc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fechar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // f41
            // 
            this.f41.AgrupaOutrosTerceiros = false;
            this.f41.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.f41.Audit = "mnuTabFornecedores";
            this.f41.AutoComplete = false;
            this.f41.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f41.CampoChave = "Fornecedor";
            this.f41.CampoChaveFisica = "";
            this.f41.CampoDescricao = "Nome";
            this.f41.Caption = "Fornecedor:";
            this.f41.CarregarValoresEdicao = false;
            this.f41.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.Fornecedores;
            this.f41.ChaveFisica = "";
            this.f41.ChaveNumerica = false;
            this.f41.F4Modal = false;
            this.f41.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f41.IDCategoria = "Fornecedores";
            this.f41.Location = new System.Drawing.Point(3, 28);
            this.f41.MaxLengthDescricao = 0;
            this.f41.MaxLengthF4 = 50;
            this.f41.MinimumSize = new System.Drawing.Size(37, 21);
            this.f41.Modulo = "BAS";
            this.f41.MostraDescricao = true;
            this.f41.MostraLink = true;
            this.f41.Name = "f41";
            this.f41.PainesInformacaoRelacionada = false;
            this.f41.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f41.PermiteDrillDown = true;
            this.f41.PermiteEnabledLink = true;
            this.f41.PodeEditarDescricao = false;
            this.f41.ResourceID = 673;
            this.f41.ResourcePersonalizada = false;
            this.f41.Restricao = "";
            this.f41.SelectionFormula = "";
            this.f41.Size = new System.Drawing.Size(1429, 22);
            this.f41.TabIndex = 0;
            this.f41.TextoDescricao = "";
            this.f41.WidthEspacamento = 60;
            this.f41.WidthF4 = 1590;
            this.f41.WidthLink = 1575;
            this.f41.Load += new System.EventHandler(this.f41_Load);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckItem,
            this.OrdemFabrico,
            this.Operacao,
            this.operacaoDescricao,
            this.Artigo,
            this.Descricao,
            this.Unidade,
            this.ArmazemDest,
            this.EstadoDest,
            this.Servico,
            this.DescricaoServico,
            this.Unidade2,
            this.QtfOF,
            this.QtdSubc,
            this.QtdaSubc,
            this.Lote,
            this.Fechar});
            this.dataGridView1.Location = new System.Drawing.Point(3, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1429, 467);
            this.dataGridView1.TabIndex = 1;
            // 
            // CheckItem
            // 
            this.CheckItem.HeaderText = "";
            this.CheckItem.Name = "CheckItem";
            this.CheckItem.ReadOnly = true;
            // 
            // OrdemFabrico
            // 
            this.OrdemFabrico.HeaderText = "OrdemFabrico";
            this.OrdemFabrico.Name = "OrdemFabrico";
            this.OrdemFabrico.ReadOnly = true;
            // 
            // Operacao
            // 
            this.Operacao.HeaderText = "Operaçao";
            this.Operacao.Name = "Operacao";
            this.Operacao.ReadOnly = true;
            this.Operacao.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Operacao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // operacaoDescricao
            // 
            this.operacaoDescricao.HeaderText = "Operação Descrição";
            this.operacaoDescricao.Name = "operacaoDescricao";
            this.operacaoDescricao.ReadOnly = true;
            this.operacaoDescricao.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.operacaoDescricao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Artigo
            // 
            this.Artigo.HeaderText = "Artigo";
            this.Artigo.Name = "Artigo";
            this.Artigo.ReadOnly = true;
            this.Artigo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Artigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Descricao
            // 
            this.Descricao.HeaderText = "Descrição";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            this.Descricao.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Descricao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Unidade
            // 
            this.Unidade.HeaderText = "Unidade";
            this.Unidade.Name = "Unidade";
            this.Unidade.ReadOnly = true;
            // 
            // ArmazemDest
            // 
            this.ArmazemDest.HeaderText = "Armazém Dest.";
            this.ArmazemDest.Name = "ArmazemDest";
            this.ArmazemDest.ReadOnly = true;
            // 
            // EstadoDest
            // 
            this.EstadoDest.HeaderText = "Estado Destino";
            this.EstadoDest.Name = "EstadoDest";
            this.EstadoDest.ReadOnly = true;
            // 
            // Servico
            // 
            this.Servico.HeaderText = "Serviço";
            this.Servico.Name = "Servico";
            this.Servico.ReadOnly = true;
            // 
            // DescricaoServico
            // 
            this.DescricaoServico.HeaderText = "Descrição Serviço";
            this.DescricaoServico.Name = "DescricaoServico";
            this.DescricaoServico.ReadOnly = true;
            // 
            // Unidade2
            // 
            this.Unidade2.HeaderText = "Unidade";
            this.Unidade2.Name = "Unidade2";
            this.Unidade2.ReadOnly = true;
            // 
            // QtfOF
            // 
            this.QtfOF.HeaderText = "Qtf. OF";
            this.QtfOF.Name = "QtfOF";
            this.QtfOF.ReadOnly = true;
            // 
            // QtdSubc
            // 
            this.QtdSubc.HeaderText = "Qtd. Subc.";
            this.QtdSubc.Name = "QtdSubc";
            this.QtdSubc.ReadOnly = true;
            // 
            // QtdaSubc
            // 
            this.QtdaSubc.HeaderText = "Qtd. a Subc.";
            this.QtdaSubc.Name = "QtdaSubc";
            this.QtdaSubc.ReadOnly = true;
            // 
            // Lote
            // 
            this.Lote.HeaderText = "Lote";
            this.Lote.Name = "Lote";
            this.Lote.ReadOnly = true;
            // 
            // Fechar
            // 
            this.Fechar.HeaderText = "Fechar";
            this.Fechar.Name = "Fechar";
            this.Fechar.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1435, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(141, 22);
            this.toolStripButton1.Text = "Gerar Subcontratação";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(73, 22);
            this.toolStripButton2.Text = "Atualizar";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // EditorEnvioSubcontratacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.f41);
            this.Name = "EditorEnvioSubcontratacao";
            this.Size = new System.Drawing.Size(1435, 526);
            this.Text = "Envio para Subcontratação";
            this.Load += new System.EventHandler(this.EditorEnvioSubcontratacao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PRISDK100.F4 f41;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrdemFabrico;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operacao;
        private System.Windows.Forms.DataGridViewTextBoxColumn operacaoDescricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Artigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArmazemDest;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoDest;
        private System.Windows.Forms.DataGridViewTextBoxColumn Servico;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescricaoServico;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unidade2;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtfOF;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtdSubc;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtdaSubc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lote;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fechar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}