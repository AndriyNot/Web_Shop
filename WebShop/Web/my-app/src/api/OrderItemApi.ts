import axios from "axios";

// Створюємо базовий інстанс для запитів
const api = axios.create({
  baseURL: "https://localhost:5001/api/OrderItem", // Вкажіть ваш API URL
  headers: {
    "Content-Type": "application/json",
  },
});

// Типи для OrderItem
export interface OrderItem {
  id: number;
  productId: number;
  quantity: number;
  price: number;
  // Інші поля вашого OrderItem
}

// API функції

// Отримати всі OrderItems
export const getAllOrderItems = async (): Promise<OrderItem[]> => {
  const response = await api.get<OrderItem[]>("/");
  return response.data;
};

// Отримати OrderItem за ID
export const getOrderItemById = async (id: number): Promise<OrderItem> => {
  const response = await api.get<OrderItem>(`/${id}`);
  return response.data;
};

// Додати новий OrderItem
export const addOrderItem = async (orderItem: OrderItem): Promise<OrderItem> => {
  const response = await api.post<OrderItem>("/", orderItem);
  return response.data;
};

// Оновити OrderItem
export const updateOrderItem = async (id: number, orderItem: OrderItem): Promise<void> => {
  await api.put(`/${id}`, orderItem);
};

// Видалити OrderItem
export const deleteOrderItem = async (id: number): Promise<void> => {
  await api.delete(`/${id}`);
};
