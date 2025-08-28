// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

if (document.body.id === "client-area") {

    const phoneInput = document.getElementById('phoneInput');
    phoneInput.focus();

    phoneInput.addEventListener('input', function (e) {
        const raw = e.target.value.replace(/\D/g, '').slice(0, 11); // Remove tudo que não for dígito, limita a 11

        let formatted = '';

        if (raw.length > 0) {
            formatted += '(' + raw.substring(0, 2);
        }

        if (raw.length >= 3 && raw.length <= 10) {
            // Formato de telefone fixo
            formatted += ') ' + raw.substring(2, 6);
            if (raw.length >= 7) {
                formatted += '-' + raw.substring(6, 10);
            }
        } else if (raw.length > 10) {
            // Formato de celular
            formatted += ') ' + raw.substring(2, 7);
            formatted += '-' + raw.substring(7, 11);
        }
        
        e.target.value = formatted;
    });


    phoneInput.addEventListener('keydown', function (e) {
        const allowedKeys = ['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight', 'Tab', 'Enter'];
        if (!allowedKeys.includes(e.key) && !/\d/.test(e.key)) {
            e.preventDefault();
        }
    });
}

///Cadastro
if (document.body.id === "cadastro") {
    flatpickr("#nascimento", {
        dateFormat: "d/m",         // usa Y apenas internamente
        allowInput: true,
        currentYearElement: false, // desabilita o seletor de ano


        onReady: function (_selectedDates, _dateStr, instance) {
            // Oculta o seletor de ano
            if (instance.currentYearElement) {
                instance.currentYearElement.style.display = "none";
                instance.currentYearElement.style.visibility = "hidden";
            }
        },

        onChange: function (selectedDates, _dateStr, instance) {
            if (selectedDates.length > 0) {
                const date = selectedDates[0];
                const day = String(date.getDate()).padStart(2, '0');
                const month = String(date.getMonth() + 1).padStart(2, '0');
                instance.input.value = `${day}/${month}`;
            }
        }
    });


    const phoneInput = document.getElementById('phoneInput');
    const CEPInput = document.getElementById('CEPInput');
    const CPFInput = document.getElementById('CPFInput');
    const NomeInput = document.getElementById('NomeInput');
    const NumeroInput = document.getElementById('NumeroInput');
    const CompInput = document.getElementById('CompInput');

    phoneInput?.addEventListener('input', function (e) {
        const raw = e.target.value.replace(/\D/g, '').slice(0, 11);
        let formatted = '';
        if (raw.length > 0) formatted += '(' + raw.substring(0, 2);
        if (raw.length >= 3) formatted += ') ' + raw.substring(2, 7);
        if (raw.length >= 8) formatted += '-' + raw.substring(7, 11);
        e.target.value = formatted;
    });

    phoneInput?.addEventListener('keydown', keyOnlyNumbers);
    CEPInput?.addEventListener('input', function (e) {
        const raw = e.target.value.replace(/\D/g, '').slice(0, 8);
        let formatted = '';
        if (raw.length > 0) formatted += raw.substring(0, 5);
        if (raw.length >= 6) formatted += '-' + raw.substring(5, 8);
        e.target.value = formatted;
    });

    CEPInput?.addEventListener('keydown', keyOnlyNumbers);
    CPFInput?.addEventListener('input', function (e) {
        const raw = e.target.value.replace(/\D/g, '').slice(0, 11);
        let formatted = '';
        if (raw.length > 0) formatted += raw.substring(0, 3);
        if (raw.length >= 4) formatted += '.' + raw.substring(3, 6);
        if (raw.length >= 7) formatted += '.' + raw.substring(6, 9);
        if (raw.length >= 10) formatted += '-' + raw.substring(9, 11);
        e.target.value = formatted;
    });

    CPFInput?.addEventListener('keydown', keyOnlyNumbers);

    NomeInput?.addEventListener('keydown', function (e) {
        if (!['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight', 'Tab', 'Enter'].includes(e.key) && /\d/.test(e.key)) {
            e.preventDefault();
        }
    });

    NumeroInput?.addEventListener('keydown', keyOnlyNumbers);
    CompInput?.addEventListener('input', e => e.target.value = e.target.value.toUpperCase());

    function keyOnlyNumbers(e) {
        const allowedKeys = ['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight', 'Tab', 'Enter'];
        if (!allowedKeys.includes(e.key) && !/\d/.test(e.key)) {
            e.preventDefault();
        }
    }
}


if (document.body.id === "pedido") {
    ///Carrinho de compras

    //On Load - Carrega itens do buffer
    document.addEventListener('DOMContentLoaded', async function () {

        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
        console.log("Chamando Servidor");

        const query = new URLSearchParams({
            __RequestVerificationToken: token
        }).toString();

        const response = await fetch(`?handler=LoadBuffer&${query}`);

        const data = await response.json();
        if (Array.isArray(data)) {
            data.forEach(item => {
                adicionarProdutoVisualmente(item.nome, item.preco, item.quantidade);
            });
        } else if (Array.isArray(data.value)) {
            data.value.forEach(item => {
                adicionarProdutoVisualmente(item.nome, item.preco, item.quantidade);
            });
        } else {
            console.error("Formato inesperado:", data);
        }
    });


    function toggleCarrinho() {
        const overlay = document.getElementById('carrinhoOverlay');
        overlay.style.display = overlay.style.display === 'none' || overlay.style.display === '' ? 'block' : 'none';
    }

    document.getElementById('carrinhoToggle').addEventListener('click', toggleCarrinho);
    ///Fim Carrinho de compras

    ///Ajuste de quantidade
    document.getElementById('up').addEventListener('click', function (e) {
        e.preventDefault();
        const input = this.closest('.input-number-wrapper').querySelector('input');
        input.stepUp();
        atualizarPrecoFinal(parseInt(input.value));
    });

    document.getElementById('down').addEventListener('click', function (e) {
        e.preventDefault();
        const input = this.closest('.input-number-wrapper').querySelector('input');
        input.stepDown();
        if (parseInt(input.value) < 1) input.value = 1;
        atualizarPrecoFinal(parseInt(input.value));
    });


    document.getElementById('input-number').addEventListener('input', function () {
        const value = parseInt(this.value, 10);
        if (value < 1 || isNaN(value)) {
            //remove elemento
            this.value = 1;
        }
        atualizarPrecoFinal(parseInt(this.value));
    });

    document.getElementById('input-number').addEventListener('blur', function () {
        if (this.value === '') {
            this.value = 1;
            atualizarPrecoFinal(1);
        }
    });


    function atualizarPrecoFinal(qtd) {
        
        const precoUnit = parseFloat(document.getElementById('input-number').dataset.precoUnitario.replace(',', '.'));
        if (typeof precoUnit == 'string') {
            parseFloat(precoUnit);
        }
        const precoFinal = precoUnit * qtd;
        document.getElementById('Item-preco').value = 'R$ ' + precoFinal.toFixed(2).replace('.', ',');
    }

    ///Fim ajuste de quantidade

    ///formataçao de valores


    ///Fim formatação de valores

    //const CarrinhoList = [];
    //CarrinhoList.push(criarItem("Camisa", 39.90, 2));
    //CarrinhoList.push(criarItem("Tênis", 129.90, 1));
    const emptyItem = { nome: "", preco: "", qtd: 0 };
    var current = emptyItem;

    ///Seleção de produtos
    document.querySelectorAll('.produto-box').forEach(produto => {
        produto.addEventListener('click', () => {
            current = emptyItem; // Reseta o item atual
            const nome = produto.dataset.nome;
            const preco = produto.dataset.preco;


            document.getElementById('input-number').value = 1; // Reseta a quantidade
            document.getElementById('Item-nome').value = "";
            document.getElementById('Item-preco').value = "";

            document.getElementById('Item-nome').value = nome;
            document.getElementById('Item-preco').value = 'R$ ' + preco;
            document.getElementById('input-number').setAttribute('data-preco-unitario', preco);
            current = { nome, preco, qtd: 1 };
        });
    });
    ///Fim seleção de produtos

    // Função para adicionar um item ao pedido via fetch

    async function enviarProdutoAoServidor(nome, preco, qtd = 1) {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            const formData = new URLSearchParams();
            formData.append("nome", nome);
            formData.append("preco", preco);
            formData.append("qtd", qtd);
            formData.append("__RequestVerificationToken", token);
            console.log(`Enviando produto: ${nome}, preço: ${preco*qtd}, quantidade: ${qtd}`);
            const response = await fetch('?handler=AdicionarProduto', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                body: formData
            });

            const data = await response.json();
            console.log("Resposta do servidor:", data);
           
            if (data.sucesso) {
                data.preco = parseFloat(data.preco.replace('R$ ', '').replace(',', '.'));
                adicionarProdutoVisualmente(data.nome, data.preco, data.quantidade);
            } else {
                alert("Erro: " + data.mensagem);
            }
        } catch (error) {
            console.error("Erro ao enviar produto:", error);
            alert("Erro inesperado ao adicionar o produto.");
        }
    }

    // Função para atualizar a lista visual no carrinho flutuante
    function adicionarProdutoVisualmente(nome, preco, qtd) {
        
        const lista = document.getElementById('orderList');
        const itens = lista.querySelectorAll('li');
        console.log(`Adicionando produto: ${nome}, preço: ${preco}, quantidade: ${qtd}`);
        let itemExistente = null;

        itens.forEach(li => {
            const texto = li.querySelector('.item-info')?.textContent || '';
            if (texto.includes(nome)) {
                itemExistente = li;
            }
        });

        if (!itemExistente) {
            preco = !(typeof preco === 'number')
                ? preco
                : preco;
            console.log(`preço: ${preco}`);
            const li = document.createElement('li');
            li.className = 'list-group-item d-flex justify-content-between align-items-center';
            li.dataset.nome = nome;
            li.dataset.qtd = qtd;
            li.dataset.preco = preco;
            li.id = "CarrinhoList";

            li.innerHTML = `
              <span class="item-info">
                ${qtd}x ${nome}
              </span>
              <span class="d-flex align-items-center">
                <span class="badge bg-success rounded-pill me-1 preco">R$
                  ${(preco*qtd).toLocaleString('pt-BR', {
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2
                        })}
                </span>
                <button type="button" class="btn-JS btn-editar" data-editando="false">✏️</button>
              </span>
            `;

            lista.appendChild(li);

            lista.appendChild(li);
            
        } else {
            // Já existe — atualiza quantidade e soma preço
            const spanInfo = itemExistente.querySelector('.item-info');
            const value = document.getElementById('CarrinhoList');
            value.dataset.qtd = qtd;
            console.log(`Atualizando produto: ${nome}, preço: ${preco}, quantidade: ${value.dataset.qtd}`);
            spanInfo.innerHTML = `
            ${qtd}x ${nome}`;
            const info = itemExistente.querySelector('.preco')
            info.innerHTML = `
            <span>R$
                ${(preco * qtd).toLocaleString('pt-BR', {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                })}
            </span>`;
        }
    }

    document.addEventListener('click', Edit);

    document.addEventListener('keydown', function (e) {
        if (e.key === "Enter") {
            const inputAtivo = document.activeElement;

            // Verifica se o foco está em um input de edição
            if (inputAtivo && inputAtivo.classList.contains('input-qtd')) {
                inputAtivo.blur(); // Opcional: tira o foco visualmente

                // Dispara clique no botão de edição associado
                const li = inputAtivo.closest('li');
                const botao = li.querySelector('.btn-editar');

                if (botao && botao.dataset.editando === "true") {
                    botao.click(); // Simula clique no ✅    
                }
            }
        }
    });

    function Edit(e) {
        const botao = e.target.closest('.btn-editar');
        if (!botao) return;

        const li = botao.closest('li');
        const editando = botao.dataset.editando === "true";

        const nomeAtual = li.dataset.nome;
        const precoUnitario = parseFloat(li.dataset.preco.replace(',', '.'));
        const quantidadeAtual = parseInt(li.dataset.qtd);
        console.log(`Editando: ${nomeAtual}, Preço Unitário: ${precoUnitario}, Quantidade Atual: ${li.dataset.qtd}`);

        if (!editando) {
            // ✅ Recalcula o valor unitário com base na fórmula X = total / quantidade
            botao.dataset.nome = nomeAtual;
            botao.dataset.precoUnitario = precoUnitario.toString(); // importante para manter como string
            botao.dataset.editando = "true";

            li.querySelector('.item-info').innerHTML = `
            <input type="number" min="1" value="${quantidadeAtual}" class="form-control form-control-sm input-qtd"/>
            <span class="item-nome ms-1">${nomeAtual}</span>
            `;

            botao.textContent = "✅";

        } else {
            const novaQtd = parseInt(li.querySelector('.input-qtd').value);
            const nome = botao.dataset.nome;
            const precoTotal = precoUnitario * novaQtd;

            li.querySelector('.preco').innerHTML = `
            R$ ${precoTotal.toFixed(2).replace('.', ',')}
            `;
            li.querySelector('.item-info').innerHTML = `
            ${ novaQtd }x ${ nome } `;
            const info = li.querySelector('.preco')
            info.innerHTML = `
            R$
                ${
                (precoUnitario * novaQtd).toLocaleString('pt-BR', {
                    minimumFractionDigits: 2,
                    maximumFractionDigits: 2
                })
            }
            </span >`;

            botao.textContent = "✏️";
            botao.dataset.editando = "false";
            li.dataset.qtd = novaQtd;
            ServerEditOrder(nome, precoUnitario.toFixed(2).replace('.', ','), novaQtd);
        }
    } 


    async function ServerEditOrder(nome, _preco, qtd=1) {
        try {
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

            const formData = new URLSearchParams();
            formData.append("nome", nome);
            formData.append("qtd", qtd);
            formData.append("__RequestVerificationToken", token);

            const response = await fetch('?handler=EditProduto', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                body: formData
            });
        } catch (error) {
            console.error("Erro ao enviar produto:", error);
            alert("Erro inesperado ao adicionar o produto.");
        }
    }

    // Listener do formulário de adição de produto
    document.getElementById('form-add-produto')?.addEventListener('submit', function (e) {
        e.preventDefault();
        console.log("Enviando produto ao servidor");
        current.qtd = parseInt(document.getElementById('input-number').value.trim());

        console.log(`Nome: ${current.nome}, Preço: ${current.preco}, Quantidade: ${current.qtd}`);
        if (!current.nome || !current.preco || current.qtd <= 0) {
            alert("Preencha nome, preço e quantidade válidos.");
            return;
        }
        enviarProdutoAoServidor(current.nome, current.preco, current.qtd);
    });


    document.getElementById('searchInput')?.addEventListener('input', function () {
        const termo = this.value.trim().toLowerCase();

        document.querySelectorAll('.produto-box').forEach(produto => {
            const nome = produto.getAttribute('data-nome')?.toLowerCase() || '';

            if (nome.includes(termo)) {
                produto.style.display = 'flex'; // ou 'block', conforme seu layout
            } else {
                produto.style.display = 'none';
            }
        });
    });


}

