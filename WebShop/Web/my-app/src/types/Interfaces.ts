// src/types/Product.ts

export interface Category {
  id: number;
  name?: string;
  products?: Product[];
}

export interface Brand {
  id: number;
  name?: string;
  country?: string;
  products?: Product[];
}

export interface OrderItem {
  id: number;
  orderId: number;
  productId: number;
  quantity: number;
  price: number;
  product?: Product;
}

export interface OrderEntity {
  id: number;
  customerName?: string;
  address?: string;
  orderItems?: OrderItem[];
}

export interface User {
  id: number;
  name?: string;
  passwordHash?: string;
  email?: string;
  updatedAt?: Date;
  buyList?: Product[];
  wishList?: Product[];
}

export interface Product {
  id: number;
  title?: string;
  description?: string;
  price: number;
  categoryId: number;
  brandId: number;
  category?: Category;
  brand?: Brand;
  orderItems?: OrderItem[];
  buyList?: User[];
  wishList?: User[];
}
