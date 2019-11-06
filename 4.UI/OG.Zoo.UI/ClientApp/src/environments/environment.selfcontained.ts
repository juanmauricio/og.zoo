function getBaseUrl() {
  return `${document.getElementsByTagName('base')[0].href}api`;
}

export const environment = {
  production: true,
  apiZoo: getBaseUrl(),
  key: 'zoo'
};
