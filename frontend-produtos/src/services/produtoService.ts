import axios from 'axios';
import { Produto } from '../types/Produto';

const API_URL = "http://localhost:5074/api/produtos";

axios.interceptors.response.use(
  response => response,
  error => {
    console.error('Erro na requisição:', error);
    return Promise.reject(error);
  }
);

export const getProdutos = async (): Promise<Produto[]> => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error: any) {
    console.error("Erro ao buscar produtos:", error);
    throw error;
  }
};

export const getProduto = async (id: number): Promise<Produto> => {
  try {
    const response = await axios.get(`${API_URL}/${id}`);
    return response.data;
  } catch (error: any) {
    console.error(`Erro ao buscar produto com ID ${id}:`, error);
    throw error;
  }
};

export const createProduto = async (produto: Omit<Produto, 'Id'>): Promise<Produto> => {
  try {
    const response = await axios.post(API_URL, produto);
    return response.data;
  } catch (error: any) {
    console.error("Erro ao criar produto:", error);
    throw error;
  }
};

export const updateProduto = async (id: number, produto: Produto): Promise<void> => {
  try {
    await axios.put(`${API_URL}/${id}`, produto);
  } catch (error: any) {
    console.error(`Erro ao atualizar produto com ID ${id}:`, error);
    throw error;
  }
};

export const deleteProduto = async (id: number): Promise<void> => {
  try {
    await axios.delete(`${API_URL}/${id}`);
  } catch (error: any) {
    console.error(`Erro ao excluir produto com ID ${id}:`, error);
    throw error;
  }
};