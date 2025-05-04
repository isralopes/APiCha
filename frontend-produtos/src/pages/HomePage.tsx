import { Typography, Box } from '@mui/material';

export const HomePage = () => {
  return (
    <Box sx={{ p: 2 }}>
      <Typography variant="h3" gutterBottom>
        Bem-vindo ao Sistema de Produtos
      </Typography>
      <Typography variant="body1">
        Este é um sistema para gerenciar produtos. Navegue até a página de produtos para começar.
      </Typography>
    </Box>
  );
};