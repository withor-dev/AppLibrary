// Arquivo: src/store/useBookStore.ts
import { create } from "zustand";
import type { Book, BookCreateDTO, BookUpdateDTO } from "../models/Book";
import api from "../services/api";

interface BookStore {
  books: Book[];
  isLoading: boolean;
  fetchBooks: () => Promise<void>;
  addBook: (data: BookCreateDTO) => Promise<void>;
  updateBook: (data: BookUpdateDTO) => Promise<void>;
  deleteBook: (id: string) => Promise<void>;
}

export const useBookStore = create<BookStore>((set, get) => ({
  books: [],
  isLoading: false,

  fetchBooks: async () => {
    set({ isLoading: true });
    try {
      const response = await api.get<Book[]>("/books");
      set({ books: response.data, isLoading: false });
    } catch (error) {
      set({ isLoading: false });
      console.error("Falha ao buscar livros", error);
    }
  },

  addBook: async (data: BookCreateDTO) => {
    await api.post("/books", data);
    await get().fetchBooks();
  },

  // NOVA FUNÇÃO: Editar
  updateBook: async (data: BookUpdateDTO) => {
    await api.put(`/books/${data.id}`, data);
    await get().fetchBooks();
  },

  // NOVA FUNÇÃO: Excluir
  deleteBook: async (id: string) => {
    await api.delete(`/books/${id}`);
    await get().fetchBooks();
  },
}));
