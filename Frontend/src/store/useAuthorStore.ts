import { create } from "zustand";
import type {
  Author,
  AuthorCreateDTO,
  AuthorUpdateDTO,
} from "../models/Author";
import api from "../services/api";

interface AuthorStore {
  authors: Author[];
  isLoading: boolean;
  fetchAuthors: () => Promise<void>;
  addAuthor: (data: AuthorCreateDTO) => Promise<void>;
  updateAuthor: (data: AuthorUpdateDTO) => Promise<void>;
  deleteAuthor: (id: string) => Promise<void>;
}

export const useAuthorStore = create<AuthorStore>((set, get) => ({
  authors: [],
  isLoading: false,

  fetchAuthors: async () => {
    set({ isLoading: true });
    try {
      const response = await api.get<Author[]>("/authors");
      set({ authors: response.data, isLoading: false });
    } catch (error) {
      set({ isLoading: false });
      console.error("Falha ao buscar autores", error);
    }
  },

  addAuthor: async (data: AuthorCreateDTO) => {
    await api.post("/authors", data);
    await get().fetchAuthors();
  },

  updateAuthor: async (data: AuthorUpdateDTO) => {
    await api.put(`/authors/${data.id}`, data);
    await get().fetchAuthors();
  },

  deleteAuthor: async (id: string) => {
    await api.delete(`/authors/${id}`);
    await get().fetchAuthors();
  },
}));
