export async function sendAnswers(user, answers, stageNumber) {
    const response = await fetch('/api/QAction/SendAnswers', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Answers: answers,
            User: user,
            stageNumber: stageNumber
        })
    })
    return await response.json();
}