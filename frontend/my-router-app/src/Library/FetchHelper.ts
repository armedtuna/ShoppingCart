// todo-at: method to call and return multiple requests? 

export function fetchJson<ReturnType>(url: string, onFetched: (data: ReturnType) => void) {
    fetch(url /*, {
        method: 'GET',
        mode: 'cors',
    }*/)
        // todo-at: handle HTTP status code?
        .then((response) => response?.json())
        .then((data) => {
            onFetched(data)
        })
        .catch((err) => {
            console.log(err.message)
        })
}

export function postJson<ReturnType>(url: string, body: any, onPosted: (data: ReturnType) => void) {
    // todo-at: are all these fetch options needed?
    const options = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body)
    }
    fetch(url, options)
        .then((response) => {
            const contentType = response.headers.get("content-type")
            if (contentType?.indexOf("application/json")) {
                return response.json()
            }
        })
        .then((data) =>
            onPosted(data)
        )
        .catch((err) => {
            console.log(err.message)
        })
}

export function deleteJson<ReturnType>(url: string, onDeleted: (data: ReturnType) => void) {
    const options = {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    }
    fetch(url, options)
        .then((response) => {
            const contentType = response.headers.get("content-type")
            if (contentType?.indexOf("application/json")) {
                return response.json()
            }
        })
        .then((data) =>
            onDeleted(data)
        )
        .catch((err) => {
            console.log(err.message)
        })
}
