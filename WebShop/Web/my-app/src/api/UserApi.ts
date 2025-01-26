import axios from "axios";

// Створюємо базовий інстанс для запитів
const api = axios.create({
  baseURL: "https://localhost:5001/api/v1/User", // Вкажіть свій API URL
  headers: {
    "Content-Type": "application/json",
  },
});

// Типи для моделі користувача та реєстрації
export interface User {
  id: number;
  name: string;
  email: string;
}

export interface RegistrationModel {
  name: string;
  email: string;
  password: string;
}

export interface Product {
  id: number;
  name: string;
  price: number;
}

// API функції

// Отримати користувача за ID
export const getUserById = async (id: number): Promise<User> => {
  const response = await api.get<User>(`/GetUserById/${id}`);
  return response.data;
};

// Створити нового користувача
export const addUser = async (model: RegistrationModel): Promise<User> => {
  const response = await api.post<User>("/AddUser", model);
  return response.data;
};

// Оновити користувача
export const updateUser = async (id: number, user: User): Promise<void> => {
  await api.put(`/UpdateUser/${id}`, user);
};

// Видалити користувача
export const deleteUser = async (id: number): Promise<void> => {
  await api.delete(`/DeleteUser/${id}`);
};

// Додати товар в список покупок
export const addToBuyList = async (userId: number, productId: number): Promise<void> => {
  await api.post(`/AddToBuyList/${userId}/${productId}`);
};

// Додати товар в список бажаних товарів
export const addToWishList = async (userId: number, productId: number): Promise<void> => {
  await api.post(`/AddToWishList/${userId}/${productId}`);
};

// Отримати список покупок
export const getBuyList = async (userId: number): Promise<Product[]> => {
  const response = await api.get<Product[]>(`/GetBuyList/${userId}`);
  return response.data;
};

// Отримати список бажаних товарів
export const getWishList = async (userId: number): Promise<Product[]> => {
  const response = await api.get<Product[]>(`/GetWishList/${userId}`);
  return response.data;
};
