import { create } from "zustand";
import type { Genre, GenreCreateDTO, GenreUpdateDTO } from "../models/Genre";
import api from "../services/api";

interface GenreStore {
  genres: Genre[];
  isLoading: boolean;
  fetchGenres: () => Promise<void>;
  addGenre: (data: GenreCreateDTO) => Promise<void>;
  updateGenre: (data: GenreUpdateDTO) => Promise<void>;
  deleteGenre: (id: string) => Promise<void>;
}

export const useGenreStore = create<GenreStore>((set, get) => ({
  genres: [],
  isLoading: false,

  fetchGenres: async () => {
    set({ isLoading: true });
    try {
      const response = await api.get<Genre[]>("/genres");
      set({ genres: response.data, isLoading: false });
    } catch (error) {
      set({ isLoading: false });
      console.error("Falha ao buscar gêneros", error);
    }
  },

  addGenre: async (data: GenreCreateDTO) => {
    await api.post("/genres", data);
    await get().fetchGenres();
  },

  updateGenre: async (data: GenreUpdateDTO) => {
    await api.put(`/genres/${data.id}`, data);
    await get().fetchGenres();
  },

  deleteGenre: async (id: string) => {
    await api.delete(`/genres/${id}`);
    await get().fetchGenres();
  },
}));
