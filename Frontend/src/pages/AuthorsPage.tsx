import { useEffect, useState } from "react";
import { useAuthorStore } from "../store/useAuthorStore";
import type { Author } from "../models/Author";

export default function AuthorsPage() {
  const {
    authors,
    isLoading,
    fetchAuthors,
    addAuthor,
    updateAuthor,
    deleteAuthor,
  } = useAuthorStore();

  const [showForm, setShowForm] = useState(false);
  const [editingId, setEditingId] = useState<string | null>(null);
  const [name, setName] = useState("");
  const [isSaving, setIsSaving] = useState(false);

  useEffect(() => {
    fetchAuthors();
  }, [fetchAuthors]);

  const resetForm = () => {
    setShowForm(false);
    setEditingId(null);
    setName("");
  };

  const handleEdit = (author: Author) => {
    setName(author.name);
    setEditingId(author.id);
    setShowForm(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!name.trim()) return;

    setIsSaving(true);
    try {
      if (editingId) {
        await updateAuthor({ id: editingId, name });
      } else {
        await addAuthor({ name });
      }
      resetForm();
    } catch (error) {
      console.error(error);
    } finally {
      setIsSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm("Tem certeza que deseja excluir este autor?")) {
      try {
        await deleteAuthor(id);
      } catch (error) {
        console.error(error);
      }
    }
  };

  return (
    <div>
      <h2 className="page-title">Lista de Autores</h2>

      {!showForm && (
        <button
          className="btn btn-primary"
          style={{ marginBottom: "20px" }}
          onClick={() => setShowForm(true)}
        >
          + Novo Autor
        </button>
      )}

      {showForm && (
        <div className="form-card">
          <h3>{editingId ? "Editar Autor" : "Cadastrar Autor"}</h3>
          <form onSubmit={handleSubmit}>
            <div className="form-group" style={{ marginTop: "15px" }}>
              <label>Nome do Autor</label>
              <input
                type="text"
                className="form-control"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Ex: J.K. Rowling"
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
        <p>Carregando autores...</p>
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
              {authors.length === 0 ? (
                <tr>
                  <td colSpan={2} style={{ textAlign: "center" }}>
                    Nenhum autor cadastrado.
                  </td>
                </tr>
              ) : (
                authors.map((author) => (
                  <tr key={author.id}>
                    <td>{author.name}</td>
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
                        onClick={() => handleEdit(author)}
                      >
                        Editar
                      </button>
                      <button
                        className="btn btn-danger"
                        onClick={() => handleDelete(author.id)}
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
