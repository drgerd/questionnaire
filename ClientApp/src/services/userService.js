    export async function loginUserAndStart(email, firstName, secondName) {
        const response = await fetch('/api/QUser/SignUp', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                Email: email,
                FirstName: firstName,
                LastName: secondName
            })
        })
        return await response.json();
    }