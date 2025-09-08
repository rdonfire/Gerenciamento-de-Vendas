using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Gerenciador_de_Vendas.Class
{
    public class DataeHora
    {
        private DispatcherTimer data;

        private Label? _labelParaAtualizar;
        public void atualizarDataHora(Label labelParaAtualizar)
        {
            _labelParaAtualizar = labelParaAtualizar;

            data = new DispatcherTimer();
            data.Interval = TimeSpan.FromSeconds(1);
            data.Tick += timer_Tick;
            data.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime DataHoraAtual = DateTime.Now;

            string dataHoraFormatada = DataHoraAtual.ToString("dd/MM/yyyy HH:mm:ss");

            if (_labelParaAtualizar != null)
            {
                _labelParaAtualizar.Content = $"{dataHoraFormatada}\nCarioca Style Company\nAdm";
            }

        }
    }
}
