import axios from "axios";

// Створюємо базовий інстанс для запитів
const api = axios.create({
  baseURL: "https://localhost:5001/api/v1/Auth", // Вкажіть ваш API URL
  headers: {
    "Content-Type": "application/json",
  },
});

// Типи для реєстрації та логіну
export interface RegistrationModel {
  username: string;
  email: string;
  password: string;
}

export interface LoginModel {
  username: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  username: string;
  // Інші поля відповіді (наприклад, інформація про користувача)
}

// API функції

// Реєстрація користувача
export const registerUser = async (registrationModel: RegistrationModel): Promise<AuthResponse> => {
  const response = await api.post<AuthResponse>("/register", registrationModel);
  return response.data;
};

// Логін користувача
export const loginUser = async (loginModel: LoginModel): Promise<AuthResponse> => {
  const response = await api.post<AuthResponse>("/login", loginModel);
  return response.data;
};

// Вихід користувача
export const logoutUser = async (): Promise<string> => {
  const response = await api.post<string>("/logout");
  return response.data;
};
