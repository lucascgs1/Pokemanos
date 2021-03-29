import { LocalStorageSaveOptions, HttpOptions } from './cache';

describe('LocalStorageSaveOptions', () => {
  it('should create an instance', () => {
    expect(new LocalStorageSaveOptions()).toBeTruthy();
  });
});

describe('HttpOptions', () => {
  it('should create an instance', () => {
    expect(new HttpOptions()).toBeTruthy();
  });
});
