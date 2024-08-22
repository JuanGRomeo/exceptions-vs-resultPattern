import { check, group } from 'k6';
import http from 'k6/http';
import { Counter } from 'k6/metrics';

export const options = {
    stages: [
        { duration: '10s', target: 20 }, // simulate ramp-up of traffic from 1 to 20 users over 10 seconds.
        { duration: '50s', target: 20 }, // stay at 20 users for 50 seconds
    ]
};

// Custom counters for V1 and V2 requests
const v1Requests = new Counter('v1_requests');
const v2Requests = new Counter('v2_requests');

function testV1() {
    const url = 'http://localhost:5299/v1/books';

    const request = {
        title: '',
        author: ''
    };

    const response = http.post(url, JSON.stringify(request), {
        headers: {
            'Content-Type': 'application/json',
        },
    });

    v1Requests.add(1);

    check(response, {
        'V1: response code was 400': (response) => response.status === 400,
    });
}

function testV2() {
    const url = 'http://localhost:5299/v2/books/';

    const request = {
        title: '',
        author: ''
    };

    const response = http.post(url, JSON.stringify(request), {
        headers: {
            'Content-Type': 'application/json',
        },
    });

    v2Requests.add(1);

    check(response, {
        'V2: response code was 400': (response) => response.status === 400,
    });
}

export default function () {
    group('Test V1 Endpoints', function () {
        testV1();
    });

    group('Test V2 Endpoints', function () {
        testV2();
    });
}

//export default function () {
//    const url = 'http://localhost:5299/books/5';

//    const response = http.get(url);

//    check(response, {
//        'response code was 404': (response) => response.status === 404,
//    });
//}
