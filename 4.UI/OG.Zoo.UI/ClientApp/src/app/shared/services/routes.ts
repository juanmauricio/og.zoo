export interface Route {
    name: string;
    url: string;
}

export const Routes: Route[] = [
    { name: 'Users', url: '/security/users/' },
    { name: 'Animals', url: '/params/animals/' },
    { name: 'Infirmary', url: '/params/infirmaries/' },
];
