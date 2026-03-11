export interface Author {
  id: string;
  name: string;
}

export interface AuthorCreateDTO {
  name: string;
}

export interface AuthorUpdateDTO {
  id: string;
  name: string;
}
