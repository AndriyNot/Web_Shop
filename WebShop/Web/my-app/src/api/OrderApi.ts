import axios from "axios";

// Створюємо базовий інстанс для запитів
const api = axios.create({
  baseURL: "https://localhost:5001/api/v1/Order", // Вкажіть ваш API URL
  headers: {
    "Content-Type": "application/json",
  },
});

// Типи для Order
export interface Order {
  id: number;
  // Додайте всі інші поля вашого Order
  customerId: number;
  orderDate: string;
  totalAmount: number;
  // інші поля
}

// API функції

// Отримати всі замовлення
export const getAllOrders = async (): Promise<Order[]> => {
  const response = await api.get<Order[]>("/GetAllOrders");
  return response.data;
};

// Отримати замовлення за ID
export const getOrderById = async (id: number): Promise<Order> => {
  const response = await api.get<Order>(`/GetOrderById/${id}`);
  return response.data;
};

// Додати нове замовлення
export const addOrder = async (order: Order): Promise<Order> => {
  const response = await api.post<Order>("/AddOrder", order);
  return response.data;
};

// Оновити замовлення
export const updateOrder = async (id: number, order: Order): Promise<void> => {
  await api.put(`/UpdateOrder/${id}`, order);
};

// Видалити замовлення
export const deleteOrder = async (id: number): Promise<void> => {
  await api.delete(`/DeleteOrder/${id}`);
};
