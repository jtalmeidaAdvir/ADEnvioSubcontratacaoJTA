using CmpBE100;
using IntBE100;
using InvBE100;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using Primavera.Extensibility.Integration.Modules.PayablesReceivables.Services;
using PrimaveraSDK;
using PRISDK100;
using StdBE100;
using StdBESql100;
using StdPlatBS100;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADEnvioSubcontratacaoJTA
{
    public partial class EditorEnvioSubcontratacao : CustomForm
    {
        private bool controlsInitialized;
        private string OrdemFabricoo { get; set; }
        private double QuantidadeGerar { get; set; }
        public EditorEnvioSubcontratacao()
        {
            InitializeComponent();
        }

        private void f41_Load(object sender, EventArgs e)
        {
            PriSDKContext.Initialize(BSO, PSO);
            InitializeSDKControls();
        }

        private void InitializeSDKControls()
        {
            //Initializes controls
            if (!controlsInitialized)
            {
                f41.Inicializa(PriSDKContext.SdkContext);
                controlsInitialized = true;
            }
        }

        private void EditorEnvioSubcontratacao_Load(object sender, EventArgs e)
        {
            CriaLotes();
            GetDadosGrid();
        }

        private void GetDadosGrid()
        {
            DateTime hoje = DateTime.Today;

            // Gera o dia 10 do mês atual
            DateTime dia10MesAtual = new DateTime(hoje.Year, hoje.Month, 10);

            // Subtrai 3 meses para obter a data inicial
            DateTime dataInicio = dia10MesAtual.AddMonths(-3);

            // Soma 3 meses para obter a data final
            DateTime dataFim = dia10MesAtual.AddMonths(3);

            // Monta o SQL com as datas formatadas
            string querydados = $@"
SELECT NULL AS GROUP1,NULL AS GROUP2,CAST(0 AS BIT) AS Checked,GOF.IDOrdemFabrico,GOF.OrdemFabrico,GOFO.IDOrdemFabricoOperacao,GOFO.ID,OFA.IDOrdemFabricoArtigo,GOFO.IDCentroTrabalho,CONVERT(NVARCHAR(50), Operacao) AS Operacao,GOFO.Descricao AS DescOperacao,OFA.Artigo,OA.Descricao AS ArtigoDescricao,OA.TratamentoDim,OA.TratamentoSeries,OA.UnidadeBase AS 'UN',OA.ArmazemSugestao,OA.LocalizacaoSugestao,CT.Armazem,CT.Localizacao,EstadoInventario,GOFO.ArtigoServico,AA.Descricao ArtServDescricao,AA.UnidadeBase,OFA.QtOF AS QtOrdemFabrico,ISNULL(DX.QTopSUB,0) QtSub,CASE WHEN OFA.QtOF - ISNULL(DX.QTopSUB,0) < 0 THEN 0 ELSE OFA.QtOF - ISNULL(DX.QTopSUB,0) END AS QtGerar,CAST(1 AS BIT) AS Fechar,NULL AS DATAFILLCOL
FROM GPR_OrdemFabrico GOF
    INNER JOIN GPR_OrdemFabricoOperacoes GOFO ON GOFO.IDOrdemFabrico = GOF.IDOrdemFabrico
    INNER JOIN Artigo AA ON AA.Artigo = GOFO.ArtigoServico
    INNER JOIN GPR_OrdemFabricoArtigos OFA ON OFA.IDOrdemFabrico = GOF.IDOrdemFabrico
    INNER JOIN Artigo OA ON OA.Artigo = OFA.Artigo
    INNER JOIN GPR_CentrosTrabalho CT ON CT.IdCentroTrabalho = GOFO.IdCentroTrabalho
    LEFT JOIN (
        SELECT IDOrdemFabrico, IDOrdemFabricoOperacao, IDOrdemFabricoArtigo,
               SUM(QtPrevista) AS QTopSUB, 
               SUM(CAST(EnvioFechado AS INTEGER)) AS EnvioFechado
        FROM GPR_DocumentosRel
        WHERE Modulo = 'ECF'
        GROUP BY IDOrdemFabrico, IDOrdemFabricoOperacao, IDOrdemFabricoArtigo
    ) DX ON DX.IDOrdemFabrico = GOF.IDOrdemFabrico 
        AND DX.IDOrdemFabricoOperacao = GOFO.IDOrdemFabricoOperacao 
        AND DX.IDOrdemFabricoArtigo = OFA.IDOrdemFabricoArtigo
WHERE GOFO.SubContratacao = 1
    AND GOF.Estado IN (2, 3)
    AND GOF.Confirmada = 1
    AND GOFO.Estado IN (7, 8)
    AND GOFO.Confirmada = 1
    AND ISNULL(DX.EnvioFechado, 0) = 0
    AND GOF.DataOrdemFabrico BETWEEN '{dataInicio:yyyy-MM-dd}' AND '{dataFim:yyyy-MM-dd}'
ORDER BY GOFO.DataIniPrevista ASC";


            var dadosSql = BSO.Consulta(querydados);

            var numerosRegitos = dadosSql.NumLinhas();
           // MessageBox.Show($"Número de registos: {numerosRegitos}", "Informação", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            if (numerosRegitos > 0)
            {
                dataGridView1.Rows.Clear(); // limpa antes de adicionar novas

                dadosSql.Inicio();
                dataGridView1.ReadOnly = false;
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
                dataGridView1.Columns[0].ReadOnly = false;
                for (int i = 0; i < numerosRegitos; i++)
                {
                    // Lê os dados
                    bool checkItem = dadosSql.DaValor<bool>("Checked");
                    OrdemFabricoo = dadosSql.DaValor<string>("OrdemFabrico");
                    string operacao = dadosSql.DaValor<string>("Operacao");
                    string descOperacao = dadosSql.DaValor<string>("DescOperacao");
                    string artigo = dadosSql.DaValor<string>("Artigo");
                    string artigoDescricao = dadosSql.DaValor<string>("ArtigoDescricao");
                    string unidadeBase = dadosSql.DaValor<string>("UN");
                    string armazem = dadosSql.DaValor<string>("Armazem");
                    string estadoInventario = dadosSql.DaValor<string>("EstadoInventario");
                    string servico = dadosSql.DaValor<string>("ArtigoServico");
                    string servicoDescricao = dadosSql.DaValor<string>("ArtServDescricao");
                    string unidadeServico = dadosSql.DaValor<string>("UnidadeBase");
                    decimal qtOrdemFabrico = dadosSql.DaValor<decimal>("QtOrdemFabrico");
                    decimal qtSub = dadosSql.DaValor<decimal>("QtSub");
                    QuantidadeGerar = dadosSql.DaValor<double>("QtGerar");
                    //--LOTE--//
                    string lote = dadosSql.DaValor<string>("OrdemFabrico");
                    bool fechar = dadosSql.DaValor<bool>("Fechar");

                    // Adiciona linha manualmente (ordem deve bater com as colunas no Designer)
                    dataGridView1.Rows.Add(checkItem, OrdemFabricoo, operacao, descOperacao, artigo, artigoDescricao, unidadeBase,
                        armazem, estadoInventario, servico, servicoDescricao, unidadeServico, qtOrdemFabrico, qtSub, QuantidadeGerar, lote, fechar);
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].ReadOnly = false;

                    dadosSql.Seguinte();
                }
            }
            else
            {
                dataGridView1.Rows.Clear(); // limpa tudo se não houver dados
            }

        }

        private void CriaLotes()
        {
            // Consulta para encontrar lotes novos
            var query = $@"SELECT artigo, OrdemFabrico AS Lote, '1' AS Activo 
                       FROM GPR_OrdemFabrico
                       WHERE OrdemFabrico NOT IN (SELECT lote FROM ArtigoLote)";
            var lotesNovos = BSO.Consulta(query);

            // Verifica o número de lotes novos
            var numLinhas = lotesNovos.NumLinhas();

            if (numLinhas == 0)
            {
            }
            else
            {

                // Move o cursor para o início do conjunto de resultados
                lotesNovos.NoInicio();

                // Percorre cada registro em lotesNovos e insere na tabela ArtigoLote
                for (int i = 0; i < numLinhas; i++)
                {
                    // Extrai os dados do registro atual
                    string artigo = lotesNovos.Valor("artigo").ToString();
                    string lote = lotesNovos.Valor("Lote").ToString();
                    string activo = lotesNovos.Valor("Activo").ToString();

                    // Cria a consulta de inserção para cada lote
                    var insertQuery = $@"
                                    INSERT INTO ArtigoLote (artigo, lote, Activo)
                                    VALUES ('{artigo}', '{lote}', '{activo}')";

                    // Executa a inserção
                    BSO.DSO.ExecuteSQL(insertQuery);

                    // Avança para o próximo registro
                    lotesNovos.Seguinte();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            dataGridView1.EndEdit();
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            if (string.IsNullOrEmpty(f41.Text))
            {
                PSO.MensagensDialogos.MostraMensagem(
                    StdPlatBS100.StdBSTipos.TipoMsg.PRI_Detalhe,
                    "Por favor, selecione uma entidade.",
                    StdBE100.StdBETipos.IconId.PRI_Critico
                );
                return;
            }

            // Verifica se há alguma linha selecionada (checkbox marcado)
            var linhasSelecionadas = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(row => row.Cells[0].Value != null && (bool)row.Cells[0].Value)
                .ToList();

            if (!linhasSelecionadas.Any())
            {
                PSO.MensagensDialogos.MostraMensagem(StdPlatBS100.StdBSTipos.TipoMsg.PRI_Detalhe, "Por favor, selecione pelo menos uma linha para gerar os documentos.",
                    StdBE100.StdBETipos.IconId.PRI_Critico);
                return;
            }

            var idECF = CriarECF(); // <- retorna o ID do documento criado
            var idTRASB = CriarTRASB();
            var idVGTSB = CriarVGTSB();
            InsertGPR_DocumentosRel(idECF, idTRASB, idVGTSB); 
            CriarES();
            AtualizaOF();
            GetDadosGrid();


        }

        private string CriarECF()
        {
            var entidade = f41.Text;

            var linhasSelecionadas = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(row => row.Cells[0].Value != null && (bool)row.Cells[0].Value)
                .ToList();

            if (!linhasSelecionadas.Any())
            {
               // MessageBox.Show("Nenhuma linha foi selecionada para gerar ECF.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            var query = "SELECT TOP 1 Serie FROM SeriesCompras WHERE TIpoDoc = 'ECF' AND SeriePorDefeito = 1 ORder By DataInicial DESC";
            var resultado = BSO.Consulta(query).DaValor<string>("Serie");
            CmpBEDocumentoCompra cmpBEDocumentoCompra = new CmpBEDocumentoCompra
            {
                Tipodoc = "ECF",
                Entidade = entidade,
                TipoEntidade = "F",
                Serie = resultado
            };

            BSO.Compras.Documentos.PreencheDadosRelacionados(cmpBEDocumentoCompra);

            int linhaIndex = 1;
            foreach (var row in linhasSelecionadas)
            {
                string ordemFabrico = row.Cells["OrdemFabrico"].Value.ToString();
                double quantidadeGerar = Convert.ToDouble(row.Cells["QtdaSubc"].Value);
                string artigoServico = row.Cells["Servico"].Value.ToString();

                string projeto = GetProjeto(ordemFabrico);

                BSO.Compras.Documentos.AdicionaLinha(cmpBEDocumentoCompra, artigoServico);

                var linha = cmpBEDocumentoCompra.Linhas.GetEdita(linhaIndex);
                linha.Quantidade = quantidadeGerar;
                linha.IDObra = projeto;

                linhaIndex++;
            }

            BSO.Compras.Documentos.Actualiza(cmpBEDocumentoCompra);

            //MessageBox.Show("Documento ECF criado com todas as linhas selecionadas!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return cmpBEDocumentoCompra.ID; // <- retorna o ID aqui
        }

        private void InsertGPR_DocumentosRel(string idECF,string idTRASB, string idVGTSB)
        {
            if (string.IsNullOrEmpty(idECF))
            {
                //MessageBox.Show("ID do documento ECF inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var linhasSelecionadas = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(row => row.Cells[0].Value != null && (bool)row.Cells[0].Value)
                .ToList();

            if (!linhasSelecionadas.Any())
            {
               // MessageBox.Show("Nenhuma linha foi selecionada para gerar ES.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var row in linhasSelecionadas)
            {
                try
                {
                    string ordemFabrico = row.Cells["OrdemFabrico"].Value?.ToString();
                    double quantidadeGerar = Convert.ToDouble(row.Cells["QtdaSubc"].Value);

                    if (string.IsNullOrWhiteSpace(ordemFabrico) || quantidadeGerar <= 0)
                        continue;

                    string dataCriacao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string artigoServico = row.Cells["Servico"].Value.ToString();
                    string artigo = row.Cells["Artigo"].Value.ToString();

                    var idOrdemFabricoSql = $"SELECT IDOrdemFabrico FROM GPR_OrdemFabrico WHERE OrdemFabrico = '{ordemFabrico}'";
                    var idOrdemFabrico = BSO.Consulta(idOrdemFabricoSql).DaValor<string>("IDOrdemFabrico");

                    var idOperacaoSql = $"SELECT IDOrdemFabricoOperacao FROM GPR_OrdemFabricoOperacoes WHERE IDOrdemFabrico = '{idOrdemFabrico}'";
                    var idOperacao = BSO.Consulta(idOperacaoSql).DaValor<string>("IDOrdemFabricoOperacao") ?? "0";

                    var idArtigoSql = $"SELECT IDOrdemFabricoArtigo FROM GPR_OrdemFabricoArtigos WHERE IDOrdemFabrico = '{idOrdemFabrico}'";
                    var idArtigo = BSO.Consulta(idArtigoSql).DaValor<string>("IDOrdemFabricoArtigo") ?? "0";

                    var idComponente = 0; // continuar como estava

                    string queryInsertECF = $@"
                INSERT INTO GPR_DocumentosRel (
                    [ID],
                    [DataCriacao],
                    [Modulo],
                    [IDOrdemFabrico],
                    [IDOrdemFabricoComponente],
                    [IDOrdemFabricoOperacao],
                    [IDOrdemFabricoArtigo],
                    [Artigo],
                    [QtPrevista],
                    [QtRecepcionada],
                    [IDCabec],
                    [IDCabec2],
                    [IDCabec3],
                    [IDLinha],
                    [Processado],
                    [EnvioFechado]
                ) VALUES (
                    '{idECF}',
                    '{dataCriacao}',
                    'ECF',
                    '{idOrdemFabrico}',
                    '{idComponente}',
                    '{idOperacao}',
                    '{idArtigo}',
                    '{artigoServico}',
                    {quantidadeGerar.ToString(CultureInfo.InvariantCulture)},
                    0,
                    '{idECF}',
                    NULL,
                    NULL,
                    '{idECF}',
                    0,
                    1
                )";

                    string queryInsertTRASB = $@"
    INSERT INTO GPR_DocumentosRel (
                    [ID],
                    [DataCriacao],
                    [Modulo],
                    [IDOrdemFabrico],
                    [IDOrdemFabricoComponente],
                    [IDOrdemFabricoOperacao],
                    [IDOrdemFabricoArtigo],
                    [Artigo],
                    [QtPrevista],
                    [QtRecepcionada],
                    [IDCabec],
                    [IDCabec2],
                    [IDCabec3],
                    [IDLinha],
                    [Processado],
                    [EnvioFechado],
                    [IDLinhaOrigem]
                ) VALUES (
                    '{idECF}',
                    '{dataCriacao}',
                    'TRASB',
                    '{idOrdemFabrico}',
                    '{idComponente}',
                    '{idOperacao}',
                    '{idArtigo}',
                    '{artigo}',
                    {quantidadeGerar.ToString(CultureInfo.InvariantCulture)},
                    0,
                    NULL,
                    '{idTRASB}',
                    '{idVGTSB}',
                    '{idECF}',
                    0,
                    0,
                    '{idECF}'
                )";

                    BSO.DSO.ExecuteSQL(queryInsertECF);
                    BSO.DSO.ExecuteSQL(queryInsertTRASB);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao inserir GPR_DocumentosRel: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string CriarTRASB()
        {
            var entidade = f41.Text;

            var linhasSelecionadas = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(row =>
                {
                    var val = row.Cells[0].Value;
                    return val != null && val is bool marcado && marcado;
                })
                .ToList();

            if (!linhasSelecionadas.Any())
            {
               // MessageBox.Show("Nenhuma linha foi selecionada para gerar TRASB.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            var invBEDocumentoTransf = new InvBEDocumentoTransf
            {
                Tipodoc = "TRASB",
                Entidade = entidade,
                TipoEntidade = "F"
            };

            BSO.Inventario.Transferencias.PreencheDadosRelacionados(invBEDocumentoTransf);

            foreach (var row in linhasSelecionadas)
            {
                try
                {
                    string artigo = row.Cells["Artigo"].Value?.ToString();
                    string estado = "DISP";
                    double quantidade = Convert.ToDouble(row.Cells["QtdaSubc"].Value);
                    string lote = row.Cells["OrdemFabrico"].Value?.ToString();
                    string armazemOrigem = "A1";
                    string armazemDestino = "A1";

                    if (string.IsNullOrWhiteSpace(artigo) || quantidade <= 0)
                        continue;

                    BSO.Inventario.Transferencias.AdicionaLinhaOrigem(
                        invBEDocumentoTransf,
                        artigo,
                        armazemOrigem,
                        armazemDestino,
                        estado,
                        quantidade,
                        lote
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao processar linha: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            string erros = "";
            BSO.Inventario.Transferencias.Actualiza(invBEDocumentoTransf, ref erros);


            return invBEDocumentoTransf.ID; // <-- Aqui está o ID do TRASB
        }

        private void AtualizaOF()
        {
            var linhasSelecionadas = dataGridView1.Rows
            .Cast<DataGridViewRow>()
            .Where(row =>
            {
                var val = row.Cells[0].Value;
                return val != null && val is bool marcado && marcado;
            })
            .ToList();

            if (!linhasSelecionadas.Any())
            {
               // MessageBox.Show("Nenhuma linha foi selecionada para gerar ES.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var row in linhasSelecionadas)
            {
                try
                {
                    string ordemFabrico = row.Cells["OrdemFabrico"].Value?.ToString();

                    // Atualiza a Ordem de Fabrico
                    string queryUpdate = $@"UPDATE GPR_OrdemFabrico
                                        SET Estado = '3',
                                            DataIniReal = GETDATE()          
                                        WHERE OrdemFabrico = '{ordemFabrico}'";

                    BSO.DSO.ExecuteSQL(queryUpdate);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar Ordem de Fabrico: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void CriarES()
        {
            var entidade = f41.Text;

            var linhasSelecionadas = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(row =>
                {
                    var val = row.Cells[0].Value;
                    return val != null && val is bool marcado && marcado;
                })
                .ToList();

            if (!linhasSelecionadas.Any())
            {
               // MessageBox.Show("Nenhuma linha foi selecionada para gerar ES.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            IntBEDocumentoInterno intBEDocumentoInterno = new IntBEDocumentoInterno
            {
                Tipodoc = "ES",
                DataVencimento = DateTime.Today,

            };
            BSO.Internos.Documentos.PreencheDadosRelacionados(intBEDocumentoInterno);

            int linhaIndex = 1;
            foreach (var row in linhasSelecionadas)
            {
                try
                {
                    string ordemFabrico = row.Cells["OrdemFabrico"].Value?.ToString();
                    string artigo = row.Cells["Artigo"].Value?.ToString();
                    double quantidadeGerar = Convert.ToDouble(row.Cells["QtdaSubc"].Value);

                    if (string.IsNullOrWhiteSpace(artigo) || quantidadeGerar <= 0)
                        continue;

                    string projeto = GetProjeto(ordemFabrico);

                    BSO.Internos.Documentos.AdicionaLinha(intBEDocumentoInterno, artigo);

                    var linha = intBEDocumentoInterno.Linhas.GetEdita(linhaIndex);
                    linha.Quantidade = quantidadeGerar;
                    linha.Lote = ordemFabrico; 
                    linha.IDObra = projeto;

                    linhaIndex++;
                }
                catch (Exception ex)
                {
                //    MessageBox.Show($"Erro ao processar linha: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            BSO.Internos.Documentos.Actualiza(intBEDocumentoInterno);
   
        }

        private string CriarVGTSB()
        {
            var entidade = f41.Text;

            var linhasSelecionadas = dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(row =>
                {
                    var val = row.Cells[0].Value;
                    return val != null && val is bool marcado && marcado;
                })
                .ToList();

            if (!linhasSelecionadas.Any())
            {
              //  MessageBox.Show("Nenhuma linha foi selecionada para gerar VGTSB.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            var query = "SELECT TOP 1 Serie FROM SeriesCompras WHERE TIpoDoc = 'VGTSB' AND SeriePorDefeito = 1 ORder By DataInicial DESC";
            var resultado = BSO.Consulta(query).DaValor<string>("Serie");
            var cmpBEDocumentoCompra = new CmpBEDocumentoCompra
            {
                Tipodoc = "VGTSB",
                Entidade = entidade,
                TipoEntidade = "F",
                Serie = resultado,
                LocalOperacao = "0",
            };

            BSO.Compras.Documentos.PreencheDadosRelacionados(cmpBEDocumentoCompra);
            var numDocExterno = cmpBEDocumentoCompra.NumDoc;
            cmpBEDocumentoCompra.NumDocExterno = numDocExterno.ToString();

            int linhaIndex = 1;
            foreach (var row in linhasSelecionadas)
            {
                try
                {
                    string ordemFabrico = row.Cells["OrdemFabrico"].Value?.ToString();
                    string artigo = row.Cells["Artigo"].Value?.ToString();
                    double quantidadeGerar = Convert.ToDouble(row.Cells["QtdaSubc"].Value);

                    if (string.IsNullOrWhiteSpace(artigo) || quantidadeGerar <= 0)
                        continue;

                    string projeto = GetProjeto(ordemFabrico);

                    BSO.Compras.Documentos.AdicionaLinha(cmpBEDocumentoCompra, artigo);

                    var linha = cmpBEDocumentoCompra.Linhas.GetEdita(linhaIndex);
                    linha.Quantidade = quantidadeGerar;
                    linha.IDObra = projeto;

                    linhaIndex++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao processar linha: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            BSO.Compras.Documentos.Actualiza(cmpBEDocumentoCompra);



            drillDownDocumento(PSO, "C", "VGTSB", cmpBEDocumentoCompra.NumDoc, cmpBEDocumentoCompra.Serie, "000");
        

            return cmpBEDocumentoCompra.ID;
        }
        public void drillDownDocumento(StdBSInterfPub PSO, string modulo, string tipoDoc, int numDoc, string serie, string filial)
        {
            try
            {

    
            StdBESqlCampoDrillDown campoDrillDown = new StdBESqlCampoDrillDown
            {
                ModuloNotificado = "GCP",
                Tipo = StdBESqlTipos.EnumTipoDrillDownListas.tddlEventoAplicacao,
                Evento = "GCP_EditarDocumento"
            };

            StdBEValoresStr param = new StdBEValoresStr();

                param.InsereNovo("Modulo", modulo);
                param.InsereNovo("Filial", filial);
                param.InsereNovo("Tipodoc", tipoDoc);
                param.InsereNovo("Serie", serie);
                param.InsereNovo("NumDocInt", Convert.ToString(numDoc));


                PSO.DrillDownLista(campoDrillDown, param);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao executar Drill Down: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GetProjeto(string ordemFabrico)
        {
            var query = $"SELECT CDU_CodigoProjeto FROM GPR_OrdemFabrico WHERE OrdemFabrico = '{ordemFabrico}'";
            var resultado = BSO.Consulta(query);
            if (resultado.NumLinhas() > 0)
            {
                resultado.NoInicio();
                var codigoProjeto = resultado.Valor("CDU_CodigoProjeto").ToString();
                var query2 = $"SELECT Id FROM COP_obras WHERE Codigo = '{codigoProjeto}'";
                var resultado2 = BSO.Consulta(query2);
                if (resultado2.NumLinhas() > 0)
                {
                    resultado2.NoInicio();
                    return resultado2.Valor("Id").ToString();
                }
            }
            return string.Empty;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CriaLotes();   
            GetDadosGrid(); 
        }


        private void AbrirEditorCompra()
        {
        }
    }
}
