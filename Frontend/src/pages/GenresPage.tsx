import { useEffect, useState } from "react";
import { useGenreStore } from "../store/useGenreStore";
import type { Genre } from "../models/Genre";

export default function GenresPage() {
  const { genres, isLoading, fetchGenres, addGenre, updateGenre, deleteGenre } =
    useGenreStore();

  const [showForm, setShowForm] = useState(false);
  const [editingId, setEditingId] = useState<string | null>(null);
  const [name, setName] = useState("");
  const [isSaving, setIsSaving] = useState(false);

  useEffect(() => {
    fetchGenres();
  }, [fetchGenres]);

  const resetForm = () => {
    setShowForm(false);
    setEditingId(null);
    setName("");
  };

  const handleEdit = (genre: Genre) => {
    setName(genre.name);
    setEditingId(genre.id);
    setShowForm(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!name.trim()) return;

    setIsSaving(true);
    try {
      if (editingId) {
        await updateGenre({ id: editingId, name });
      } else {
        await addGenre({ name });
      }
      resetForm();
    } catch (error) {
      console.error(error);
    } finally {
      setIsSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm("Tem certeza que deseja excluir este gênero?")) {
      try {
        await deleteGenre(id);
      } catch (error) {
        console.error(error);
      }
    }
  };

  return (
    <div>
      <h2 className="page-title">Lista de Gêneros</h2>

      {!showForm && (
        <button
          className="btn btn-primary"
          style={{ marginBottom: "20px" }}
          onClick={() => setShowForm(true)}
        >
          + Novo Gênero
        </button>
      )}

      {showForm && (
        <div className="form-card">
          <h3>{editingId ? "Editar Gênero" : "Cadastrar Gênero"}</h3>
          <form onSubmit={handleSubmit}>
            <div className="form-group" style={{ marginTop: "15px" }}>
              <label>Nome do Gênero</label>
              <input
                type="text"
                className="form-control"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Ex: Ficção Científica"
                required
              />
            </div>
            <div className="form-actions">
              <button
                type="submit"
                className="btn btn-primary"
                disabled={isSaving}
              >
                {isSaving ? "Salvando..." : "Salvar"}
              </button>
              <button
                type="button"
                className="btn btn-secondary"
                onClick={resetForm}
                disabled={isSaving}
              >
                Cancelar
              </button>
            </div>
          </form>
        </div>
      )}

      {isLoading ? (
        <p>Carregando gêneros...</p>
      ) : (
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>Nome</th>
                <th style={{ width: "160px", textAlign: "center" }}>Ações</th>
              </tr>
            </thead>
            <tbody>
              {genres.length === 0 ? (
                <tr>
                  <td colSpan={2} style={{ textAlign: "center" }}>
                    Nenhum gênero cadastrado.
                  </td>
                </tr>
              ) : (
                genres.map((genre) => (
                  <tr key={genre.id}>
                    <td>{genre.name}</td>
                    <td
                      style={{
                        textAlign: "center",
                        display: "flex",
                        gap: "10px",
                        justifyContent: "center",
                      }}
                    >
                      <button
                        className="btn btn-secondary"
                        onClick={() => handleEdit(genre)}
                      >
                        Editar
                      </button>
                      <button
                        className="btn btn-danger"
                        onClick={() => handleDelete(genre.id)}
                      >
                        Excluir
                      </button>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
