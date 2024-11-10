export interface SecUser {
    userID: string;
    email: string;
    passwordHash: string;
    role: string;
    status: string;
    lastLogin?: string | null;
  }
  