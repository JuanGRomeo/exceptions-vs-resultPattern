import { check } from 'k6';
import http from 'k6/http';

export const options = {
    stages: [
        { duration: '10s', target: 20 }, // simulate ramp-up of traffic from 1 to 20 users over 10 seconds.
        { duration: '50s', target: 20 }, // stay at 20 users for 50 seconds
    ]
};

export default function () {
    const url = 'http://localhost:5299/v1/books/8';

    const response = http.get(url);

    check(response, {
        'response code was 404': (response) => response.status === 404,
    });
}
