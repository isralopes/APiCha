import { Card, CardContent, Typography, Button, CardActions } from '@mui/material';
import { Produto } from '../types/Produto';

interface ProdutoItemProps {
  produto: Produto;
  onDelete: (id: number) => Promise<void>;
  onEdit: (produto: Produto) => void;
}

export const ProdutoItem = ({ produto, onDelete, onEdit }: ProdutoItemProps) => {
  const handleDelete = async () => {
    if (produto.Id === undefined) {
      console.error('ID do produto é indefinido', produto);
      return;
    }
    await onDelete(produto.Id);
  };

  return (
    <Card sx={{ minWidth: 275, marginBottom: 2 }}>
      <CardContent>
        <Typography variant="h5" component="div">
          {produto.Nome}
        </Typography>
        <Typography color="text.secondary">
          Preço: R$ {produto.Preco?.toFixed(2) || '0.00'}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small" onClick={() => onEdit(produto)}>
          Editar
        </Button>
        <Button
          size="small"
          color="error"
          onClick={handleDelete}
        >
          Excluir
        </Button>
      </CardActions>
    </Card>
  );
};