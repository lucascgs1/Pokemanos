import { Usuario, Login } from './usuario';

describe('Usuario', () => {
  it('should create an instance', () => {
    expect(new Usuario()).toBeTruthy();
  });
});

describe('Login', () => {
  it('should create an instance', () => {
    expect(new Login()).toBeTruthy();
  });
});
