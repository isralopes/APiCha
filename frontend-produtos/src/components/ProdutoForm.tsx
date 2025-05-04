import { useState, useEffect } from 'react';
import { Button, TextField, Box, Typography } from '@mui/material';
import { Produto } from '../types/Produto';

interface ProdutoFormProps {
  produto?: Produto | any;
  onSubmit: (produto: Omit<Produto, 'Id'> | Produto) => void;
  onCancel: () => void;
}

export const ProdutoForm = ({ produto, onSubmit, onCancel }: ProdutoFormProps) => {
  const [nome, setNome] = useState('');
  const [preco, setPreco] = useState('');

  useEffect(() => {
    if (produto) {
      setNome(produto.Nome || '');
      setPreco(produto.Preco !== undefined ? produto.Preco.toString() : '');
    } else {
      setNome('');
      setPreco('');
    }
  }, [produto]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const precoFloat = parseFloat(preco);
    const produtoData = {
      Nome: nome,
      Preco: isNaN(precoFloat) ? 0 : precoFloat,
      ...(produto?.Id && { Id: produto.Id })
    };
    onSubmit(produtoData);
  };

  return (
    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }}>
      <Typography variant="h6" gutterBottom>
        {produto ? 'Editar Produto' : 'Adicionar Produto'}
      </Typography>
      <TextField
        label="Nome"
        value={nome}
        onChange={(e) => setNome(e.target.value)}
        fullWidth
        margin="normal"
        required
      />
      <TextField
        label="Preço"
        type="number"
        value={preco}
        onChange={(e) => setPreco(e.target.value)}
        fullWidth
        margin="normal"
        required
        inputProps={{ step: "0.01" }}
      />
      <Box sx={{ mt: 2 }}>
        <Button type="submit" variant="contained" sx={{ mr: 2 }}>
          Salvar
        </Button>
        <Button variant="outlined" onClick={onCancel}>
          Cancelar
        </Button>
      </Box>
    </Box>
  );
};