import React, { useEffect, useState } from 'react';
import './index.css';

interface Chamado {
  chamadoId: string;
  descricao: string;
  status: string;
  criadoEm: string;
}

function App() {
  const [chamados, setChamados] = useState<Chamado[]>([]);

  useEffect(() => {
    fetch('http://localhost:5273/api/chamado/listar')
      .then(response => response.json())
      .then(data => setChamados(data))
      .catch(error => console.error('Erro ao buscar chamados:', error));
  }, []);

  return (
    <div className="container">
      <h1>Lista de Chamados</h1>
      <table>
        <thead>
          <tr>
            <th>Descrição</th>
            <th>Status</th>
            <th>Data de Criação</th>
          </tr>
        </thead>
        <tbody>
          {chamados.map(chamado => (
            <tr key={chamado.chamadoId}>
              <td>{chamado.descricao}</td>
              <td>{chamado.status}</td>
              <td>{new Date(chamado.criadoEm).toLocaleString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default App;