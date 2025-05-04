import { useState, useEffect } from 'react';
import { Box, Typography, Button, CircularProgress, Alert } from '@mui/material';
import { ProdutoForm } from './ProdutoForm';
import { ProdutoItem } from './ProdutoItem';
import { Produto } from '../types/Produto';
import { getProdutos, createProduto, updateProduto, deleteProduto as deleteProdutoApi } from '../services/produtoService';

export const ProdutoList = () => {
  const [produtos, setProdutos] = useState<Produto[]>([]);
  const [editingProduto, setEditingProduto] = useState<Produto | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    loadProdutos();
  }, []);

  const loadProdutos = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await getProdutos();
      console.log('Dados recebidos:', data);
      setProdutos(data);
    } catch (err: any) {
      console.error("Erro ao carregar produtos:", err);
      setError(err.message || "Erro ao carregar produtos. Verifique o console para mais detalhes.");
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (produtoData: Omit<Produto, 'Id'> | Produto) => {
    try {
      setError(null);
      console.log("Dados do formulário ao submeter:", produtoData);
      if (editingProduto?.Id) {
        console.log("Chamando updateProduto com ID:", editingProduto.Id, produtoData);
        await updateProduto(editingProduto.Id, produtoData as Produto); 
      } else {
        console.log("Chamando createProduto com dados:", produtoData);
        await createProduto(produtoData);
      }
      setShowForm(false);
      setEditingProduto(null);
      await loadProdutos();
    } catch (err: any) {
      console.error("Erro ao salvar produto:", err);
      setError(err.message || "Erro ao salvar produto. Verifique os dados e tente novamente.");
    }
  };

  const handleDelete = async (id: number) => {
    try {
      setError(null);
      await deleteProdutoApi(id);
      await loadProdutos();
    } catch (err: any) {
      console.error("Erro ao excluir produto:", err);
      setError(err.message || "Erro ao excluir produto. Verifique o console para mais detalhes.");
    }
  };

  const handleEdit = (produto: Produto) => {
    console.log("Produto para edição:", produto);
    setEditingProduto(produto);
    setShowForm(true);
  };

  const handleAdicionar = () => {
    setEditingProduto(null);
    setShowForm(true);
  };

  const handleCancelarForm = () => {
    setShowForm(false);
    setEditingProduto(null);
  };

  return (
    <Box sx={{ p: 2 }}>
      <Typography variant="h4" gutterBottom>
        Lista de Produtos
      </Typography>

      {error && (
        <Alert severity="error" sx={{ mb: 2 }}>
          {error}
        </Alert>
      )}

      {showForm ? (
        <ProdutoForm
          produto={editingProduto}
          onSubmit={handleSubmit}
          onCancel={handleCancelarForm}
        />
      ) : (
        <>
          <Button
            variant="contained"
            onClick={handleAdicionar}
            sx={{ mb: 2 }}
          >
            Adicionar Produto
          </Button>

          {loading ? (
            <Box display="flex" justifyContent="center" mt={4}>
              <CircularProgress />
            </Box>
          ) : produtos.length === 0 ? (
            <Typography variant="body1" color="text.secondary">
              Nenhum produto cadastrado.
            </Typography>
          ) : (
            produtos.map((produto) => (
              <ProdutoItem
                key={produto.Id} 
                produto={produto}
                onDelete={handleDelete}
                onEdit={handleEdit}
              />
            ))
          )}
        </>
      )}
    </Box>
  );
};