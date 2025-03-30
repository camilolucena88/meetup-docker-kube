import smtplib
import os
from email.mime.text import MIMEText

async def send_email(email):
    smtp_server = os.getenv("SMTP_SERVER")
    smtp_port = int(os.getenv("SMTP_PORT"))
    smtp_user = os.getenv("SMTP_USER")
    smtp_password = os.getenv("SMTP_PASSWORD")

    msg = MIMEText(f"Welcome! Your registration is complete.")
    msg["Subject"] = "Registration Success"
    msg["From"] = smtp_user
    msg["To"] = email

    try:
        server = smtplib.SMTP(smtp_server, smtp_port)
        server.starttls()
        server.login(smtp_user, smtp_password)
        server.sendmail(smtp_user, [email], msg.as_string())
        server.quit()
        print(f"Email sent")
    except Exception as e:
        print(f"Failed to send email: {e}")