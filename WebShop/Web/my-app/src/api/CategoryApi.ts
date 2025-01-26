import axios from "axios";

// Створюємо базовий інстанс для запитів
const api = axios.create({
  baseURL: "https://localhost:5001/api/v1/Category", // Вкажіть ваш API URL
  headers: {
    "Content-Type": "application/json",
  },
});

// Типи для Category
export interface Category {
  id: number;
  name: string;
  description: string;
  // Інші поля вашої категорії
}

// API функції

// Отримати всі категорії
export const getAllCategories = async (): Promise<Category[]> => {
  const response = await api.get<Category[]>("/GetAllCategories");
  return response.data;
};

// Отримати категорію за ID
export const getCategoryById = async (id: number): Promise<Category> => {
  const response = await api.get<Category>(`/GetCategoryById/${id}`);
  return response.data;
};

// Додати нову категорію
export const addCategory = async (category: Category): Promise<Category> => {
  const response = await api.post<Category>("/AddCategory", category);
  return response.data;
};

// Оновити категорію
export const updateCategory = async (id: number, category: Category): Promise<void> => {
  await api.put(`/UpdateCategory/${id}`, category);
};

// Видалити категорію
export const deleteCategory = async (id: number): Promise<void> => {
  await api.delete(`/DeleteCategory/${id}`);
};
