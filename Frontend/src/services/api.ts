import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.request.use(
  (config) => {
    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response) {
      const status = error.response.status;
      const message = error.response.data?.message || "Erro inesperado.";

      if (status === 404) {
        console.error("Recurso não encontrado (404).");
      } else if (status === 400) {
        console.error(`Erro de validação (400): ${message}`);
        alert(`Atenção: ${message}`);
      } else if (status === 500) {
        console.error(`Erro no servidor (500): ${message}`);
        alert(`Erro interno: ${message}`);
      }
    } else {
      console.error("Erro de conexão com a API.");
      alert(
        "Não foi possível conectar ao servidor. Verifique se a API está rodando.",
      );
    }

    return Promise.reject(error);
  },
);

export default api;
