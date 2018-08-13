﻿var api = 'http://localhost:53731/api/paciente/';

var tabela = document.querySelector('#perfilpaciente');

obterPaciente(idPaciente);

function update(pacientes) {
    tabela.innerHTML = template(pacientes);
}

function template(pacientes = []) {
    return `
    <table class="table table-hover table-bordered">
        <thead>
            <tr>
                <th>#</th>
                <th>Nome</th>
                <th>Cpf</th>
                <th>Data De Nascimento</th>
                <th>Email</th>
                <th>Telefone</th>
                <th>Celular</th>
                <th>Gênero</th>
            </tr>
        </thead>
        <tbody>
        ${
            pacientes.map(function(paciente){
                return `
                    <tr>
                        <td>${paciente.id}</td>
                        <td>${paciente.nome}</td>
                        <td>${paciente.cpf}</td>
                        <td>${paciente.dataNasc}</td>
                        <td>${paciente.email}</td>
                        <td>${paciente.telefone}</td>
                        <td>${paciente.celular}</td>
                        <td>${paciente.genero}</td>
                        <td>
                            <a href="#" onclick="alterarPaciente(${paciente.id})">Editar</a> | 
                            <a href="#" onclick="excluirPaciente(${paciente.id})">Excluir</a>
                        </td>
                    </tr>
                `;
            }).join('')
        }
        </tbody>
    </table>
    `;
}

function obterPaciente(idPaciente) {

    var request = new Request(api + idPaciente, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            // console.log(response);
            if (response.status == 200) {
                response.json()
                    .then(function (pacientes) {
                        update(pacientes);
                    });
            } else {
                alert("Ocorreu um erro ao obter os paciente");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

function alterarPaciente(idPaciente) {
    window.location.href = 'medicoCriar.html?id=' + idPaciente;
}

function excluirPaciente(idPaciente) {
    if (confirm('Tem certeza que deseja excluir esse paciente?')) {
        
        var request = new Request(api + idPaciente, {
            method: "DELETE",
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        fetch(request)
            .then(function (response) {
                // console.log(response);
                if (response.status == 200) {
                    obterPaciente();
                } else {
                    alert("Ocorreu um erro ao excluir o paciente");
                }
            })
            .catch(function (response) {
                // console.log(response);
                alert("Desculpe, ocorreu um erro no servidor.");
            });
    }


}