import axios from "axios";

// Створюємо базовий інстанс для запитів
const api = axios.create({
  baseURL: "https://localhost:5001/api/v1/Brand", // Вкажіть ваш API URL
  headers: {
    "Content-Type": "application/json",
  },
});

// Типи для Brand
export interface Brand {
  id: number;
  name: string;
  description: string;
  // Інші поля вашого бренду
}

// API функції

// Отримати всі бренди
export const getAllBrands = async (): Promise<Brand[]> => {
  const response = await api.get<Brand[]>("/GetAllBrands");
  return response.data;
};

// Отримати бренд за ID
export const getBrandById = async (id: number): Promise<Brand> => {
  const response = await api.get<Brand>(`/GetBrandById/${id}`);
  return response.data;
};

// Додати новий бренд
export const addBrand = async (brand: Brand): Promise<Brand> => {
  const response = await api.post<Brand>("/AddBrand", brand);
  return response.data;
};

// Оновити бренд
export const updateBrand = async (id: number, brand: Brand): Promise<void> => {
  await api.put(`/UpdateBrand/${id}`, brand);
};

// Видалити бренд
export const deleteBrand = async (id: number): Promise<void> => {
  await api.delete(`/DeleteBrand/${id}`);
};
