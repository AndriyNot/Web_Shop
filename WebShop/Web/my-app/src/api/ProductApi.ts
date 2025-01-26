import axios from "axios";

// Створюємо базовий інстанс для запитів
const api = axios.create({
  baseURL: "https://localhost:5001/api/v1/Product", // Вкажіть свій API URL
  headers: {
    "Content-Type": "application/json",
  },
});

// Типи для продукту
export interface ProductEntity {
  id: number;
  name: string;
  description: string;
  price: number;
  // Інші поля вашого продукту
}

// API функції

// Отримати всі продукти
export const getAllProducts = async (): Promise<ProductEntity[]> => {
  const response = await api.get<ProductEntity[]>("/GetAllProducts");
  return response.data;
};

// Отримати продукт за ID
export const getProductById = async (id: number): Promise<ProductEntity> => {
  const response = await api.get<ProductEntity>(`/GetProductById/${id}`);
  return response.data;
};

// Додати новий продукт
export const addProduct = async (product: ProductEntity): Promise<ProductEntity> => {
  const response = await api.post<ProductEntity>("/AddProduct", product);
  return response.data;
};

// Оновити продукт
export const updateProduct = async (id: number, product: ProductEntity): Promise<void> => {
  await api.put(`/UpdateProduct/${id}`, product);
};

// Видалити продукт
export const deleteProduct = async (id: number): Promise<void> => {
  await api.delete(`/DeleteProduct/${id}`);
};
