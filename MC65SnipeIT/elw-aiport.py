#!/usr/bin/env python3

"""
Emily's Little Warehouse (ELW) - Complete Python Edition
A Python port of the C# Windows Forms application for Snipe-IT asset management.
Originally designed for Windows Mobile handheld devices, now ported for desktop computers.

Features:
- Read asset information
- Add new items and locations  
- Move assets between locations
- Generate QR code stickers
- Search and manage inventory
- Batch operations

Author: AI Assistant
Version: 1.0.1 (Complete)
"""

import os
import json
import tkinter as tk
from tkinter import ttk, messagebox, filedialog, scrolledtext
import requests
import qrcode
from PIL import Image, ImageDraw, ImageFont, ImageTk
from datetime import datetime
import threading
import subprocess
import sys
import tempfile
import webbrowser

# ==================== CONFIGURATION MANAGER ====================

class ConfigManager:
    """Manages application configuration - replaces the C# XML config system"""
    
    def __init__(self, config_file="elw_config.json"):
        self.config_file = config_file
        self.default_config = {
            "ServerAddress": "",
            "ServerKey": "",
            "Storage": "./storage",
            "ModelID": "",
            "NyaPrint": False,
            "NyaPrintServer": "",
            "BTSend": False,
            "EarlyConnect": False,
            "AutofillAddress": False,
            "Address": "",
            "Address2": "",
            "ZipCode": "",
            "City": "",
            "Country": "",
            "Currency": "USD"
        }
        self.settings = self.load_config()
    
    def load_config(self):
        """Load configuration from file"""
        if os.path.exists(self.config_file):
            try:
                with open(self.config_file, 'r') as f:
                    config = json.load(f)
                # Merge with defaults to ensure all keys exist
                for key, value in self.default_config.items():
                    if key not in config:
                        config[key] = value
                return config
            except Exception as e:
                print(f"Error loading config: {e}")
                return self.default_config.copy()
        else:
            return self.default_config.copy()
    
    def save_config(self):
        """Save configuration to file"""
        try:
            # Create storage directory if it doesn't exist
            storage_dir = self.settings.get("Storage", "./storage")
            os.makedirs(storage_dir, exist_ok=True)
            os.makedirs(os.path.join(storage_dir, "ELW-Labels"), exist_ok=True)
            
            with open(self.config_file, 'w') as f:
                json.dump(self.settings, f, indent=4)
            return True
        except Exception as e:
            print(f"Error saving config: {e}")
            return False
    
    def get(self, key, default=None):
        """Get a configuration value"""
        return self.settings.get(key, default)
    
    def set(self, key, value):
        """Set a configuration value"""
        self.settings[key] = value

# ==================== API CLIENT ====================

class SnipeITClient:
    """API client for Snipe-IT server communication"""
    
    def __init__(self, config_manager):
        self.config = config_manager
        self.session = requests.Session()
        self.session.timeout = 20
    
    def _get_headers(self):
        """Get headers for API requests"""
        return {
            'Authorization': f'Bearer {self.config.get("ServerKey")}',
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        }
    
    def _get_base_url(self):
        """Get base URL for API requests"""
        server_address = self.config.get("ServerAddress", "").rstrip('/')
        return f"{server_address}/api/v1"
    
    def test_connection(self):
        """Test connection to Snipe-IT server"""
        try:
            url = f"{self._get_base_url()}/hardware/1"
            response = self.session.get(url, headers=self._get_headers())
            return response.status_code in [200, 404]  # 404 is OK, means server is reachable
        except Exception as e:
            print(f"Connection test failed: {e}")
            return False
    
    def get_locations(self, limit=300, offset=0):
        """Get list of locations"""
        try:
            url = f"{self._get_base_url()}/locations"
            params = {
                'limit': limit,
                'offset': offset,
                'sort': 'created_at'
            }
            response = self.session.get(url, headers=self._get_headers(), params=params)
            response.raise_for_status()
            data = response.json()
            return data.get('rows', [])
        except Exception as e:
            print(f"Error getting locations: {e}")
            return []
    
    def get_status_labels(self):
        """Get list of status labels"""
        try:
            url = f"{self._get_base_url()}/statuslabels"
            response = self.session.get(url, headers=self._get_headers())
            response.raise_for_status()
            data = response.json()
            return data.get('rows', [])
        except Exception as e:
            print(f"Error getting status labels: {e}")
            return []
    
    def get_companies(self):
        """Get list of companies"""
        try:
            url = f"{self._get_base_url()}/companies"
            response = self.session.get(url, headers=self._get_headers())
            response.raise_for_status()
            data = response.json()
            return data.get('rows', [])
        except Exception as e:
            print(f"Error getting companies: {e}")
            return []
    
    def get_users(self, limit=300, offset=0):
        """Get list of users"""
        try:
            url = f"{self._get_base_url()}/users"
            params = {
                'limit': limit,
                'offset': offset,
                'sort': 'created_at',
                'order': 'asc'
            }
            response = self.session.get(url, headers=self._get_headers(), params=params)
            response.raise_for_status()
            data = response.json()
            return data.get('rows', [])
        except Exception as e:
            print(f"Error getting users: {e}")
            return []
    
    def get_hardware(self, hardware_id):
        """Get hardware information by ID"""
        try:
            url = f"{self._get_base_url()}/hardware/{hardware_id}"
            response = self.session.get(url, headers=self._get_headers())
            response.raise_for_status()
            return response.json()
        except Exception as e:
            print(f"Error getting hardware {hardware_id}: {e}")
            return None
    
    def search_hardware(self, search_term, limit=50, offset=0):
        """Search for hardware"""
        try:
            url = f"{self._get_base_url()}/hardware"
            params = {
                'limit': limit,
                'offset': offset,
                'search': search_term
            }
            response = self.session.get(url, headers=self._get_headers(), params=params)
            response.raise_for_status()
            data = response.json()
            return data.get('rows', [])
        except Exception as e:
            print(f"Error searching hardware: {e}")
            return []
    
    def create_hardware(self, name, model_id, company_id, status_id, location_id, notes=""):
        """Create new hardware item"""
        try:
            url = f"{self._get_base_url()}/hardware"
            payload = {
                'name': name,
                'model_id': model_id,
                'company_id': company_id,
                'status_id': status_id,
                'location_id': location_id,
                'rtd_location_id': location_id,
                'notes': notes
            }
            response = self.session.post(url, headers=self._get_headers(), json=payload)
            response.raise_for_status()
            return response.json()
        except Exception as e:
            print(f"Error creating hardware: {e}")
            return None
    
    def create_location(self, name, address="", address2="", manager_id=None,
                       country="", zipcode="", city="", parent_id=None, currency="USD"):
        """Create new location"""
        try:
            url = f"{self._get_base_url()}/locations"
            payload = {
                'name': name,
                'address': address,
                'address2': address2,
                'country': country,
                'zip': zipcode,
                'city': city,
                'currency': currency
            }
            if manager_id:
                payload['manager_id'] = manager_id
            if parent_id:
                payload['parent_id'] = parent_id
                
            response = self.session.post(url, headers=self._get_headers(), json=payload)
            response.raise_for_status()
            return response.json()
        except Exception as e:
            print(f"Error creating location: {e}")
            return None
    
    def update_hardware_location(self, hardware_id, location_id):
        """Move hardware to a different location"""
        try:
            url = f"{self._get_base_url()}/hardware/{hardware_id}"
            payload = {
                'rtd_location_id': location_id
            }
            response = self.session.patch(url, headers=self._get_headers(), json=payload)
            response.raise_for_status()
            return response.json()
        except Exception as e:
            print(f"Error updating hardware location: {e}")
            return None
    
    def log_move(self, hardware_id, location_name, success=True):
        """Log move operation to file"""
        try:
            storage_dir = self.config.get("Storage", "./storage")
            log_file = os.path.join(storage_dir, "ELW-MoveLog.txt")
            timestamp = datetime.now().strftime("%Y/%m/%d %H:%M:%S")
            if success:
                message = f"{hardware_id} Moved to Loc {location_name}"
            else:
                message = f"ERROR: {hardware_id} NOT MOVED TO {location_name}"
            
            os.makedirs(storage_dir, exist_ok=True)
            with open(log_file, 'a', encoding='utf-8') as f:
                f.write(f"{timestamp} - {message}\n")
        except Exception as e:
            print(f"Error logging move: {e}")

# ==================== QR CODE GENERATOR ====================

class QRCodeGenerator:
    """QR code generation for stickers"""
    
    def __init__(self, config_manager):
        self.config = config_manager
    
    def generate_hardware_qr(self, hardware_id):
        """Generate QR code for hardware item"""
        server_address = self.config.get("ServerAddress", "").rstrip('/')
        url_to_encode = f"{server_address}/hardware/{hardware_id}"
        return self._generate_qr_code(url_to_encode)
    
    def generate_location_qr(self, location_id):
        """Generate QR code for location"""
        return self._generate_qr_code(str(location_id))
    
    def _generate_qr_code(self, data):
        """Generate QR code image"""
        try:
            qr = qrcode.QRCode(
                version=1,
                error_correction=qrcode.constants.ERROR_CORRECT_L,
                box_size=10,
                border=4,
            )
            qr.add_data(data)
            qr.make(fit=True)
            return qr.make_image(fill_color="black", back_color="white")
        except Exception as e:
            print(f"Error generating QR code: {e}")
            return None
    
    def create_hardware_sticker(self, hardware_data, qr_image):
        """Create a complete sticker image for hardware"""
        try:
            # Create base image (similar to panel size in C#)
            width, height = 400, 470
            sticker = Image.new('RGB', (width, height), 'white')
            draw = ImageDraw.Draw(sticker)
            
            # Try to load a font
            try:
                title_font = ImageFont.truetype("arial.ttf", 16)
                text_font = ImageFont.truetype("arial.ttf", 12)
            except:
                title_font = ImageFont.load_default()
                text_font = ImageFont.load_default()
            
            # Add QR code (centered at top)
            if qr_image:
                qr_resized = qr_image.resize((320, 320))
                sticker.paste(qr_resized, (40, 30))
            
            # Add text information
            y_pos = 360
            name = hardware_data.get('name', 'Unknown Item')
            asset_tag = hardware_data.get('asset_tag', '')
            location = hardware_data.get('rtd_location', {})
            location_name = location.get('name', '') if location else ''
            
            # Draw text (centered)
            draw.text((width//2, y_pos), name, font=title_font, fill='black', anchor='mt')
            draw.text((width//2, y_pos + 25), f"Asset Tag: {asset_tag}", font=text_font, fill='black', anchor='mt')
            draw.text((width//2, y_pos + 45), f"Location: {location_name}", font=text_font, fill='black', anchor='mt')
            
            return sticker
        except Exception as e:
            print(f"Error creating hardware sticker: {e}")
            return None
    
    def create_location_sticker(self, location_data, qr_image):
        """Create a complete sticker image for location"""
        try:
            # Create base image
            width, height = 400, 470
            sticker = Image.new('RGB', (width, height), 'white')
            draw = ImageDraw.Draw(sticker)
            
            # Try to load a font
            try:
                title_font = ImageFont.truetype("arial.ttf", 16)
                text_font = ImageFont.truetype("arial.ttf", 12)
            except:
                title_font = ImageFont.load_default()
                text_font = ImageFont.load_default()
            
            # Add QR code (centered at top)
            if qr_image:
                qr_resized = qr_image.resize((320, 320))
                sticker.paste(qr_resized, (40, 30))
            
            # Add text information
            y_pos = 360
            name = location_data.get('name', 'Unknown Location')
            location_id = location_data.get('id', '')
            
            # Draw text (centered)
            draw.text((width//2, y_pos), name, font=title_font, fill='black', anchor='mt')
            draw.text((width//2, y_pos + 25), f"ID: {location_id}", font=text_font, fill='black', anchor='mt')
            
            return sticker
        except Exception as e:
            print(f"Error creating location sticker: {e}")
            return None
    
    def save_sticker(self, sticker_image, filename):
        """Save sticker image to file"""
        try:
            storage_dir = self.config.get("Storage", "./storage")
            labels_dir = os.path.join(storage_dir, "ELW-Labels")
            os.makedirs(labels_dir, exist_ok=True)
            
            filepath = os.path.join(labels_dir, filename)
            sticker_image.save(filepath, "JPEG")
            return filepath
        except Exception as e:
            print(f"Error saving sticker: {e}")
            return None

# ==================== LIST MANAGER ====================

class ListManager:
    """Manages the item list functionality"""
    
    def __init__(self, config_manager):
        self.config = config_manager
    
    def add_to_list(self, location_name, asset_tag, item_name):
        """Add item to the list file"""
        try:
            storage_dir = self.config.get("Storage", "./storage")
            list_file = os.path.join(storage_dir, "ELW-List.txt")
            
            os.makedirs(storage_dir, exist_ok=True)
            
            line = f"{location_name} - {asset_tag}: {item_name}\n"
            with open(list_file, 'a', encoding='utf-8') as f:
                f.write(line)
            return True
        except Exception as e:
            print(f"Error adding to list: {e}")
            return False
    
    def get_list_contents(self):
        """Get contents of the list file"""
        try:
            storage_dir = self.config.get("Storage", "./storage")
            list_file = os.path.join(storage_dir, "ELW-List.txt")
            
            if os.path.exists(list_file):
                with open(list_file, 'r', encoding='utf-8') as f:
                    return f.read()
            return ""
        except Exception as e:
            print(f"Error reading list: {e}")
            return ""
    
    def clear_list(self):
        """Clear the list file"""
        try:
            storage_dir = self.config.get("Storage", "./storage")
            list_file = os.path.join(storage_dir, "ELW-List.txt")
            
            if os.path.exists(list_file):
                os.remove(list_file)
            return True
        except Exception as e:
            print(f"Error clearing list: {e}")
            return False

# ==================== BASE DIALOG CLASS ====================

class BaseDialog:
    def __init__(self, parent, title, size=(400, 300)):
        self.parent = parent
        self.window = tk.Toplevel(parent)
        self.window.title(title)

        # Wait until geometry info can be calculated
        self.window.update_idletasks()

        # Calculate center position
        x = (self.window.winfo_screenwidth() // 2) - (size[0] // 2)
        y = (self.window.winfo_screenheight() // 2) - (size[1] // 2)

        # Set geometry string properly
        self.window.geometry(f"{size[0]}x{size[1]}+{x}+{y}")

        self.window.transient(parent)

        # Delay grab_set to avoid errors on some systems
        self.window.after(10, lambda: self.window.grab_set())

        # Setup widgets
        self.create_widgets()

        # Schedule grab_set after the window is mapped / visible
        self.window.after(10, lambda: self.window.grab_set())

    
    def create_widgets(self):
        """Override in subclasses"""
        pass
    
    def close(self):
        """Close the dialog"""
        self.window.destroy()

# ==================== DIALOG CLASSES ====================

class AboutDialog(BaseDialog):
    """About dialog - replaces About.cs"""
    def __init__(self, parent):
        super().__init__(parent, "About Emily's Little Warehouse", (500, 400))  # provide title and size
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Cat emoji as picture placeholder
        cat_label = ttk.Label(main_frame, text="🐱", font=('Arial', 48))
        cat_label.pack(pady=10)
        
        # App info
        ttk.Label(main_frame, text="Emily Little Warehouse", 
                 font=('Arial', 14, 'bold')).pack(pady=5)
        ttk.Label(main_frame, text="V1.0.1 (Python Edition/AI)", 
                 font=('Arial', 10)).pack(pady=5)
        
        # Description
        description = """An AI Python port of the original C# application for managing Snipe-IT assets. Originally designed for Windows Mobile devices, now available for desktop computers.

Features:
• Read asset information
• Add new items and locations
• Move assets between locations  
• Generate QR code stickers
• Search and manage inventory
• Batch operations"""
        
        text_widget = tk.Text(main_frame, height=10, width=50, wrap=tk.WORD)
        text_widget.insert('1.0', description)
        text_widget.config(state='disabled')
        text_widget.pack(pady=10, fill=tk.BOTH, expand=True)
        
        # Close button
        ttk.Button(main_frame, text="~Headpats~", command=self.close).pack(pady=10)

class ServerSettingsDialog(BaseDialog):
    """Server settings dialog - replaces ServerSettings.cs"""
    
    def __init__(self, parent, config, api_client):
        self.config = config
        self.api_client = api_client
        super().__init__(parent, "ELW Server Settings", (500, 400))
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Server address
        ttk.Label(main_frame, text="Server Address:").pack(anchor=tk.W)
        self.server_entry = ttk.Entry(main_frame, width=50)
        self.server_entry.pack(fill=tk.X, pady=(0, 10))
        self.server_entry.insert(0, self.config.get("ServerAddress", ""))
        
        # API token
        ttk.Label(main_frame, text="API Token:").pack(anchor=tk.W)
        token_frame = ttk.Frame(main_frame)
        token_frame.pack(fill=tk.X, pady=(0, 10))
        
        self.token_entry = ttk.Entry(token_frame, width=40, show="*")
        self.token_entry.pack(side=tk.LEFT, fill=tk.X, expand=True)
        self.token_entry.insert(0, self.config.get("ServerKey", ""))
        
        ttk.Button(token_frame, text="Import From File", 
                  command=self.import_token).pack(side=tk.RIGHT, padx=(5, 0))
        
        # Model ID
        ttk.Label(main_frame, text="Default Model ID:").pack(anchor=tk.W)
        self.model_entry = ttk.Entry(main_frame, width=50)
        self.model_entry.pack(fill=tk.X, pady=(0, 10))
        self.model_entry.insert(0, self.config.get("ModelID", ""))
        
        # Storage path
        ttk.Label(main_frame, text="Storage Directory:").pack(anchor=tk.W)
        self.storage_entry = ttk.Entry(main_frame, width=50)
        self.storage_entry.pack(fill=tk.X, pady=(0, 10))
        self.storage_entry.insert(0, self.config.get("Storage", "./storage"))
        
        # Test connection button
        ttk.Button(main_frame, text="Test Connection", 
                  command=self.test_connection).pack(pady=10)
        
        # Status label
        self.status_label = ttk.Label(main_frame, text="Ready", foreground="blue")
        self.status_label.pack(pady=5)
        
        # Button frame
        btn_frame = ttk.Frame(main_frame)
        btn_frame.pack(fill=tk.X, pady=10)
        
        ttk.Button(btn_frame, text="Save", command=self.save_settings).pack(side=tk.RIGHT)
        ttk.Button(btn_frame, text="Cancel", command=self.close).pack(side=tk.RIGHT, padx=(0, 5))
    
    def import_token(self):
        """Import token from file"""
        filename = filedialog.askopenfilename(
            title="Select Token File",
            filetypes=[("Text files", "*.txt"), ("All files", "*.*")]
        )
        if filename:
            try:
                with open(filename, 'r') as f:
                    token = f.readline().strip()
                self.token_entry.delete(0, tk.END)
                self.token_entry.insert(0, token)
                self.status_label.config(text="Token imported successfully", foreground="green")
            except Exception as e:
                messagebox.showerror("Error", f"Failed to import token: {e}")
    
    def test_connection(self):
        """Test connection to server"""
        def test():
            self.status_label.config(text="Testing connection...", foreground="blue")
            self.window.update()
            
            # Temporarily update config for test
            old_server = self.config.get("ServerAddress")
            old_key = self.config.get("ServerKey")
            
            self.config.set("ServerAddress", self.server_entry.get().strip())
            self.config.set("ServerKey", self.token_entry.get().strip())
            
            success = self.api_client.test_connection()
            
            if success:
                self.status_label.config(text="Connection successful!", foreground="green")
            else:
                self.status_label.config(text="Connection failed!", foreground="red")
                # Restore old values
                self.config.set("ServerAddress", old_server)
                self.config.set("ServerKey", old_key)
        
        threading.Thread(target=test, daemon=True).start()
    
    def save_settings(self):
        """Save settings"""
        self.config.set("ServerAddress", self.server_entry.get().strip())
        self.config.set("ServerKey", self.token_entry.get().strip())
        self.config.set("ModelID", self.model_entry.get().strip())
        self.config.set("Storage", self.storage_entry.get().strip())
        
        if self.config.save_config():
            self.status_label.config(text="Settings saved successfully!", foreground="green")
            self.window.after(1500, self.close)
        else:
            self.status_label.config(text="Failed to save settings!", foreground="red")

class AddItemDialog(BaseDialog):
    """Add new item dialog - replaces AddItem.cs"""
    
    def __init__(self, parent, config, api_client):
        self.config = config
        self.api_client = api_client
        self.companies = []
        self.statuses = []
        self.locations = []
        super().__init__(parent, "ELW - Add Items", (500, 600))
        self.load_data()
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Name
        ttk.Label(main_frame, text="Name:").grid(row=0, column=0, sticky=tk.W, pady=2)
        self.name_entry = ttk.Entry(main_frame, width=40)
        self.name_entry.grid(row=0, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # Company
        ttk.Label(main_frame, text="Company:").grid(row=1, column=0, sticky=tk.W, pady=2)
        self.company_combo = ttk.Combobox(main_frame, width=37, state="readonly")
        self.company_combo.grid(row=1, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # Status
        ttk.Label(main_frame, text="Status:").grid(row=2, column=0, sticky=tk.W, pady=2)
        self.status_combo = ttk.Combobox(main_frame, width=37, state="readonly")
        self.status_combo.grid(row=2, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # Location
        ttk.Label(main_frame, text="Location:").grid(row=3, column=0, sticky=tk.W, pady=2)
        self.location_combo = ttk.Combobox(main_frame, width=37, state="readonly")
        self.location_combo.grid(row=3, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # Notes
        ttk.Label(main_frame, text="Notes:").grid(row=4, column=0, sticky=tk.NW, pady=2)
        self.notes_text = tk.Text(main_frame, width=40, height=4)
        self.notes_text.grid(row=4, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # Configure grid weights
        main_frame.columnconfigure(1, weight=1)
        
        # Status label
        self.status_label = ttk.Label(main_frame, text="Status: Waiting for user...", 
                                     foreground="blue")
        self.status_label.grid(row=5, column=0, columnspan=2, pady=10)
        
        # Button frame
        btn_frame = ttk.Frame(main_frame)
        btn_frame.grid(row=6, column=0, columnspan=2, sticky=tk.EW, pady=10)
        
        ttk.Button(btn_frame, text="Add", command=self.add_item).pack(side=tk.RIGHT)
        ttk.Button(btn_frame, text="Exit", command=self.close).pack(side=tk.RIGHT, padx=(0, 5))
    
    def load_data(self):
        """Load companies, statuses, and locations"""
        def load():
            try:
                # Load companies
                self.companies = self.api_client.get_companies()
                company_names = [f"{c['name']} (ID: {c['id']})" for c in self.companies]
                self.company_combo['values'] = company_names
                
                # Load statuses
                self.statuses = self.api_client.get_status_labels()
                status_names = [f"{s['name']} (ID: {s['id']})" for s in self.statuses]
                self.status_combo['values'] = status_names
                
                # Load locations
                self.locations = self.api_client.get_locations()
                location_names = [f"{l['name']} (ID: {l['id']})" for l in self.locations]
                self.location_combo['values'] = location_names
                
                self.status_label.config(text="Data loaded successfully", foreground="green")
            except Exception as e:
                self.status_label.config(text=f"Error loading data: {e}", foreground="red")
        
        threading.Thread(target=load, daemon=True).start()
    
    def add_item(self):
        """Add new item"""
        # Validate inputs
        if not self.name_entry.get().strip():
            messagebox.showerror("Error", "Name is required")
            return
        
        if not self.company_combo.get():
            messagebox.showerror("Error", "Company is required")
            return
        
        if not self.status_combo.get():
            messagebox.showerror("Error", "Status is required")
            return
        
        if not self.location_combo.get():
            messagebox.showerror("Error", "Location is required")
            return
        
        # Get IDs from combo selections
        company_id = self._extract_id(self.company_combo.get())
        status_id = self._extract_id(self.status_combo.get())
        location_id = self._extract_id(self.location_combo.get())
        model_id = self.config.get("ModelID", "")
        
        if not model_id:
            messagebox.showerror("Error", "Model ID not configured. Please set it in Server Settings.")
            return
        
        def create():
            try:
                self.status_label.config(text="Creating item...", foreground="blue")
                
                result = self.api_client.create_hardware(
                    name=self.name_entry.get().strip(),
                    model_id=model_id,
                    company_id=company_id,
                    status_id=status_id,
                    location_id=location_id,
                    notes=self.notes_text.get('1.0', tk.END).strip()
                )
                
                if result and result.get('status') == 'success':
                    payload = result.get('payload', {})
                    item_id = payload.get('id', 'Unknown')
                    self.status_label.config(text=f"Success: ID:{item_id} - Item created", 
                                           foreground="green")
                else:
                    error_msg = result.get('messages', 'Unknown error') if result else 'Failed to create'
                    self.status_label.config(text=f"Error: {error_msg}", foreground="red")
                    
            except Exception as e:
                self.status_label.config(text=f"Error: {e}", foreground="red")
        
        threading.Thread(target=create, daemon=True).start()
    
    def _extract_id(self, combo_text):
        """Extract ID from combo box text like 'Name (ID: 123)'"""
        try:
            return combo_text.split('ID: ')[1].rstrip(')')
        except:
            return ""

class AddLocationDialog(BaseDialog):
    """Add new location dialog - replaces AddLoc.cs"""
    
    def __init__(self, parent, config, api_client):
        self.config = config
        self.api_client = api_client
        self.locations = []
        self.users = []
        super().__init__(parent, "ELW - New Location", (500, 650))
        self.load_data()
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Name
        ttk.Label(main_frame, text="Name:").grid(row=0, column=0, sticky=tk.W, pady=2)
        self.name_entry = ttk.Entry(main_frame, width=40)
        self.name_entry.grid(row=0, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # Parent location checkbox and combo
        self.parent_var = tk.BooleanVar()
        self.parent_check = ttk.Checkbutton(main_frame, text="Child Location", 
                                          variable=self.parent_var,
                                          command=self.toggle_parent)
        self.parent_check.grid(row=1, column=0, sticky=tk.W, pady=2)
        
        self.parent_combo = ttk.Combobox(main_frame, width=37, state="disabled")
        self.parent_combo.grid(row=1, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # Manager
        ttk.Label(main_frame, text="Manager:").grid(row=2, column=0, sticky=tk.W, pady=2)
        self.manager_combo = ttk.Combobox(main_frame, width=37, state="readonly")
        self.manager_combo.grid(row=2, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # Address
        ttk.Label(main_frame, text="Address:").grid(row=3, column=0, sticky=tk.W, pady=2)
        self.address_entry = ttk.Entry(main_frame, width=40)
        self.address_entry.grid(row=3, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # Address 2
        ttk.Label(main_frame, text="Address 2:").grid(row=4, column=0, sticky=tk.W, pady=2)
        self.address2_entry = ttk.Entry(main_frame, width=40)
        self.address2_entry.grid(row=4, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # ZIP/City
        ttk.Label(main_frame, text="Zip/City:").grid(row=5, column=0, sticky=tk.W, pady=2)
        zip_frame = ttk.Frame(main_frame)
        zip_frame.grid(row=5, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        self.zip_entry = ttk.Entry(zip_frame, width=10)
        self.zip_entry.pack(side=tk.LEFT)
        self.city_entry = ttk.Entry(zip_frame, width=25)
        self.city_entry.pack(side=tk.LEFT, padx=(5, 0), fill=tk.X, expand=True)
        
        # Country
        ttk.Label(main_frame, text="Country:").grid(row=6, column=0, sticky=tk.W, pady=2)
        self.country_entry = ttk.Entry(main_frame, width=40)
        self.country_entry.grid(row=6, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        
        # Currency
        ttk.Label(main_frame, text="Currency:").grid(row=7, column=0, sticky=tk.W, pady=2)
        self.currency_entry = ttk.Entry(main_frame, width=40)
        self.currency_entry.grid(row=7, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
        self.currency_entry.insert(0, "USD")
        
        # Configure grid weights
        main_frame.columnconfigure(1, weight=1)
        
        # Autofill if enabled
        if self.config.get("AutofillAddress", False):
            self.address_entry.insert(0, self.config.get("Address", ""))
            self.address2_entry.insert(0, self.config.get("Address2", ""))
            self.zip_entry.insert(0, self.config.get("ZipCode", ""))
            self.city_entry.insert(0, self.config.get("City", ""))
            self.country_entry.insert(0, self.config.get("Country", ""))
            self.currency_entry.delete(0, tk.END)
            self.currency_entry.insert(0, self.config.get("Currency", "USD"))
        
        # Status label
        self.status_label = ttk.Label(main_frame, text="Status: Waiting for User...", 
                                     foreground="blue")
        self.status_label.grid(row=8, column=0, columnspan=2, pady=10)
        
        # Button frame
        btn_frame = ttk.Frame(main_frame)
        btn_frame.grid(row=9, column=0, columnspan=2, sticky=tk.EW, pady=10)
        
        ttk.Button(btn_frame, text="Create", command=self.create_location).pack(side=tk.RIGHT)
        ttk.Button(btn_frame, text="Gen Stickers", command=self.gen_stickers).pack(side=tk.RIGHT, padx=(0, 5))
        ttk.Button(btn_frame, text="Exit", command=self.close).pack(side=tk.RIGHT, padx=(0, 5))
    
    def load_data(self):
        """Load locations and users"""
        def load():
            try:
                # Load locations for parent selection
                self.locations = self.api_client.get_locations()
                location_names = [f"{l['name']} (ID: {l['id']})" for l in self.locations]
                self.parent_combo['values'] = location_names
                
                # Load users for manager selection
                self.users = self.api_client.get_users()
                user_names = [f"{u['name']} (ID: {u['id']})" for u in self.users]
                self.manager_combo['values'] = user_names
                
                self.status_label.config(text="Data loaded successfully", foreground="green")
            except Exception as e:
                self.status_label.config(text=f"Error loading data: {e}", foreground="red")
        
        threading.Thread(target=load, daemon=True).start()
    
    def toggle_parent(self):
        """Toggle parent location combo box"""
        if self.parent_var.get():
            self.parent_combo.config(state="readonly")
        else:
            self.parent_combo.config(state="disabled")
    
    def create_location(self):
        """Create new location"""
        if not self.name_entry.get().strip():
            messagebox.showerror("Error", "Name is required")
            return
        
        def create():
            try:
                self.status_label.config(text="Creating location...", foreground="blue")
                
                # Get manager ID if selected
                manager_id = None
                if self.manager_combo.get():
                    manager_id = self._extract_id(self.manager_combo.get())
                
                # Get parent ID if selected
                parent_id = None
                if self.parent_var.get() and self.parent_combo.get():
                    parent_id = self._extract_id(self.parent_combo.get())
                
                result = self.api_client.create_location(
                    name=self.name_entry.get().strip(),
                    address=self.address_entry.get().strip(),
                    address2=self.address2_entry.get().strip(),
                    manager_id=manager_id,
                    country=self.country_entry.get().strip(),
                    zipcode=self.zip_entry.get().strip(),
                    city=self.city_entry.get().strip(),
                    parent_id=parent_id,
                    currency=self.currency_entry.get().strip()
                )
                
                if result and result.get('status') == 'success':
                    self.status_label.config(text=f"Success: Location created", 
                                           foreground="green")
                else:
                    error_msg = result.get('messages', 'Unknown error') if result else 'Failed to create'
                    self.status_label.config(text=f"Error: {error_msg}", foreground="red")
                    
            except Exception as e:
                self.status_label.config(text=f"Error: {e}", foreground="red")
        
        threading.Thread(target=create, daemon=True).start()
    
    def gen_stickers(self):
        """Generate location stickers"""
        # This would open the location sticker generation dialog
        messagebox.showinfo("Info", "Location sticker generation - to be implemented with main sticker dialog")
    
    def _extract_id(self, combo_text):
        """Extract ID from combo box text like 'Name (ID: 123)'"""
        try:
            return combo_text.split('ID: ')[1].rstrip(')')
        except:
            return ""

class MoveItemDialog(BaseDialog):
    """Move single item dialog - replaces MoveItem.cs"""
    
    def __init__(self, parent, config, api_client):
        self.config = config
        self.api_client = api_client
        super().__init__(parent, "ELW - Move Items", (500, 600))
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Item URL
        ttk.Label(main_frame, text="Item URL:").pack(anchor=tk.W)
        self.url_entry = ttk.Entry(main_frame, width=50)
        self.url_entry.pack(fill=tk.X, pady=(0, 10))
        self.url_entry.bind('<Return>', lambda e: self.location_entry.focus())
        
        # Location ID
        ttk.Label(main_frame, text="Location ID:").pack(anchor=tk.W)
        self.location_entry = ttk.Entry(main_frame, width=50)
        self.location_entry.pack(fill=tk.X, pady=(0, 10))
        self.location_entry.bind('<Return>', lambda e: self.move_item())
        
        # History
        ttk.Label(main_frame, text="History:", font=('Arial', 8)).pack(anchor=tk.W)
        self.history_listbox = tk.Listbox(main_frame, height=8)
        self.history_listbox.pack(fill=tk.BOTH, expand=True, pady=(0, 10))
        
        # Button frame
        btn_frame = ttk.Frame(main_frame)
        btn_frame.pack(fill=tk.X, pady=10)
        
        ttk.Button(btn_frame, text="Move", command=self.move_item).pack(side=tk.RIGHT)
        ttk.Button(btn_frame, text="Back To Main Menu", command=self.close).pack(fill=tk.X, padx=(0, 5))
        
        # Status info
        ttk.Label(main_frame, text="Keyboard Shortcuts Disabled! - Use the keypad for data entry!", 
                 font=('Arial', 6)).pack(side=tk.BOTTOM, anchor=tk.E)
    
    def move_item(self):
        """Move item to new location"""
        if not self.url_entry.get().strip() or not self.location_entry.get().strip():
            messagebox.showerror("Error", "Both URL and Location ID are required")
            return
        
        def move():
            try:
                # Extract ID from URL
                server_address = self.config.get("ServerAddress", "").rstrip('/')
                url_pattern = f"{server_address}/hardware/"
                
                hardware_id = self.url_entry.get().strip()
                if url_pattern in hardware_id:
                    hardware_id = hardware_id.replace(url_pattern, "")
                
                location_id = self.location_entry.get().strip()
                
                result = self.api_client.update_hardware_location(hardware_id, location_id)
                
                if result and result.get('status') == 'success':
                    message = f"{hardware_id} Moved to Loc ID {location_id}"
                    self.history_listbox.insert(0, message)
                    self.api_client.log_move(hardware_id, location_id, True)
                else:
                    message = f"ERROR: {hardware_id} NOT MOVED TO {location_id}"
                    self.history_listbox.insert(0, message)
                    self.api_client.log_move(hardware_id, location_id, False)
                
                # Clear and focus URL entry for next item
                self.url_entry.delete(0, tk.END)
                self.url_entry.focus()
                
            except Exception as e:
                message = f"Error moving {hardware_id}: {e}"
                self.history_listbox.insert(0, message)
        
        threading.Thread(target=move, daemon=True).start()

class BatchMoveDialog(BaseDialog):
    """Batch move items dialog - replaces BatchMove.cs"""
    
    def __init__(self, parent, config, api_client):
        self.config = config
        self.api_client = api_client
        self.locations = []
        super().__init__(parent, "ELW - Batch Move", (500, 600))
        self.load_locations()
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Item URL
        ttk.Label(main_frame, text="Item URL:").pack(anchor=tk.W)
        self.url_entry = ttk.Entry(main_frame, width=50)
        self.url_entry.pack(fill=tk.X, pady=(0, 10))
        self.url_entry.bind('<Return>', lambda e: self.move_item())
        
        # Location
        ttk.Label(main_frame, text="Location:").pack(anchor=tk.W)
        self.location_combo = ttk.Combobox(main_frame, width=47, state="readonly")
        self.location_combo.pack(fill=tk.X, pady=(0, 10))
        self.location_combo.bind('<<ComboboxSelected>>', lambda e: self.url_entry.focus())
        
        # History
        ttk.Label(main_frame, text="History:", font=('Arial', 8)).pack(anchor=tk.W)
        self.history_listbox = tk.Listbox(main_frame, height=8)
        self.history_listbox.pack(fill=tk.BOTH, expand=True, pady=(0, 10))
        
        # Button frame
        btn_frame = ttk.Frame(main_frame)
        btn_frame.pack(fill=tk.X, pady=10)
        
        ttk.Button(btn_frame, text="Move", command=self.move_item).pack(side=tk.RIGHT)
        ttk.Button(btn_frame, text="Back To Main Menu", command=self.close).pack(fill=tk.X, padx=(0, 5))
        
        # Status info
        ttk.Label(main_frame, text="Keyboard Shortcuts Disabled! - Use the keypad for data entry!", 
                 font=('Arial', 6)).pack(side=tk.BOTTOM, anchor=tk.E)
    
    def load_locations(self):
        """Load locations into combo box"""
        def load():
            try:
                self.locations = self.api_client.get_locations()
                location_names = [f"{l['name']} (ID: {l['id']})" for l in self.locations]
                self.location_combo['values'] = location_names
            except Exception as e:
                messagebox.showerror("Error", f"Failed to load locations: {e}")
        
        threading.Thread(target=load, daemon=True).start()
    
    def move_item(self):
        """Move item to selected location"""
        if not self.url_entry.get().strip() or not self.location_combo.get():
            messagebox.showerror("Error", "Both URL and Location are required")
            return
        
        def move():
            try:
                # Extract ID from URL
                server_address = self.config.get("ServerAddress", "").rstrip('/')
                url_pattern = f"{server_address}/hardware/"
                
                hardware_id = self.url_entry.get().strip()
                if url_pattern in hardware_id:
                    hardware_id = hardware_id.replace(url_pattern, "")
                
                location_id = self._extract_id(self.location_combo.get())
                location_name = self.location_combo.get().split(' (ID:')[0]
                
                result = self.api_client.update_hardware_location(hardware_id, location_id)
                
                if result and result.get('status') == 'success':
                    message = f"{hardware_id} Moved to Loc {location_name}"
                    self.history_listbox.insert(0, message)
                    self.api_client.log_move(hardware_id, location_name, True)
                else:
                    message = f"ERROR: {hardware_id} NOT MOVED TO {location_name}"
                    self.history_listbox.insert(0, message)
                    self.api_client.log_move(hardware_id, location_name, False)
                
                # Clear URL entry and select for next item
                self.url_entry.delete(0, tk.END)
                self.url_entry.select_range(0, tk.END)
                
            except Exception as e:
                message = f"Error moving {hardware_id}: {e}"
                self.history_listbox.insert(0, message)
        
        threading.Thread(target=move, daemon=True).start()
    
    def _extract_id(self, combo_text):
        """Extract ID from combo box text like 'Name (ID: 123)'"""
        try:
            return combo_text.split('ID: ')[1].rstrip(')')
        except:
            return ""

class GenerateStickersDialog(BaseDialog):
    """Generate stickers dialog - replaces Sticker.cs and StickerLocation.cs"""
    
    def __init__(self, parent, config, api_client, qr_gen):
        self.config = config
        self.api_client = api_client
        self.qr_gen = qr_gen
        self.current_data = None
        self.sticker_image = None
        self.locations = []
        super().__init__(parent, "ELW - Sticker Generation", (550, 700))
        self.load_locations()
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Notebook for tabs
        notebook = ttk.Notebook(main_frame)
        notebook.pack(fill=tk.BOTH, expand=True)
        
        # Hardware sticker tab
        hw_frame = ttk.Frame(notebook, padding="10")
        notebook.add(hw_frame, text="Hardware Stickers")
        
        ttk.Label(hw_frame, text="Hardware ID:").pack(anchor=tk.W)
        self.hw_id_entry = ttk.Entry(hw_frame, width=50)
        self.hw_id_entry.pack(fill=tk.X, pady=(0, 10))
        self.hw_id_entry.bind('<Return>', lambda e: self.generate_hardware_sticker())
        
        ttk.Button(hw_frame, text="Generate Hardware Sticker", 
                  command=self.generate_hardware_sticker).pack(pady=5)
        
        # Location sticker tab
        loc_frame = ttk.Frame(notebook, padding="10")
        notebook.add(loc_frame, text="Location Stickers")
        
        ttk.Label(loc_frame, text="Location:").pack(anchor=tk.W)
        self.location_combo = ttk.Combobox(loc_frame, width=47, state="readonly")
        self.location_combo.pack(fill=tk.X, pady=(0, 10))
        
        ttk.Button(loc_frame, text="Generate Location Sticker", 
                  command=self.generate_location_sticker).pack(pady=5)
        
        # Preview frame
        preview_frame = ttk.LabelFrame(main_frame, text="Sticker Preview", padding="10")
        preview_frame.pack(fill=tk.BOTH, expand=True, pady=10)
        
        self.preview_label = ttk.Label(preview_frame, text="Generate a sticker to see preview")
        self.preview_label.pack(expand=True)
        
        # Info frame
        info_frame = ttk.Frame(main_frame)
        info_frame.pack(fill=tk.X, pady=5)
        
        self.info_label = ttk.Label(info_frame, text="Ready", foreground="blue")
        self.info_label.pack()
        
        # Button frame
        btn_frame = ttk.Frame(main_frame)
        btn_frame.pack(fill=tk.X, pady=10)
        
        self.save_btn = ttk.Button(btn_frame, text="Save", command=self.save_sticker, state="disabled")
        self.save_btn.pack(side=tk.RIGHT)
        
        ttk.Button(btn_frame, text="Exit", command=self.close).pack(side=tk.RIGHT, padx=(0, 5))
    
    def load_locations(self):
        """Load locations for location stickers"""
        def load():
            try:
                self.locations = self.api_client.get_locations()
                location_names = [f"{l['name']} (ID: {l['id']})" for l in self.locations]
                self.location_combo['values'] = location_names
            except Exception as e:
                self.info_label.config(text=f"Error loading locations: {e}", foreground="red")
        
        threading.Thread(target=load, daemon=True).start()
    
    def generate_hardware_sticker(self):
        """Generate sticker for hardware item"""
        hardware_id = self.hw_id_entry.get().strip()
        if not hardware_id:
            messagebox.showerror("Error", "Hardware ID is required")
            return
        
        def generate():
            try:
                self.info_label.config(text="Generating hardware sticker...", foreground="blue")
                
                # Get hardware data
                hardware_data = self.api_client.get_hardware(hardware_id)
                if not hardware_data:
                    self.info_label.config(text="Hardware not found", foreground="red")
                    return
                
                # Generate QR code
                qr_image = self.qr_gen.generate_hardware_qr(hardware_id)
                if not qr_image:
                    self.info_label.config(text="Failed to generate QR code", foreground="red")
                    return
                
                # Create sticker
                self.sticker_image = self.qr_gen.create_hardware_sticker(hardware_data, qr_image)
                if not self.sticker_image:
                    self.info_label.config(text="Failed to create sticker", foreground="red")
                    return
                
                # Update preview
                self.update_preview(self.sticker_image)
                self.save_btn.config(state="normal")
                self.current_data = {'type': 'hardware', 'id': hardware_id, 'data': hardware_data}
                self.info_label.config(text="Hardware sticker generated successfully", foreground="green")
                
            except Exception as e:
                self.info_label.config(text=f"Error: {e}", foreground="red")
        
        threading.Thread(target=generate, daemon=True).start()
    
    def generate_location_sticker(self):
        """Generate sticker for location"""
        if not self.location_combo.get():
            messagebox.showerror("Error", "Location is required")
            return
        
        def generate():
            try:
                self.info_label.config(text="Generating location sticker...", foreground="blue")
                
                location_id = self._extract_id(self.location_combo.get())
                location_name = self.location_combo.get().split(' (ID:')[0]
                
                # Create location data
                location_data = {'name': location_name, 'id': location_id}
                
                # Generate QR code
                qr_image = self.qr_gen.generate_location_qr(location_id)
                if not qr_image:
                    self.info_label.config(text="Failed to generate QR code", foreground="red")
                    return
                
                # Create sticker
                self.sticker_image = self.qr_gen.create_location_sticker(location_data, qr_image)
                if not self.sticker_image:
                    self.info_label.config(text="Failed to create sticker", foreground="red")
                    return
                
                # Update preview
                self.update_preview(self.sticker_image)
                self.save_btn.config(state="normal")
                self.current_data = {'type': 'location', 'id': location_id, 'data': location_data}
                self.info_label.config(text="Location sticker generated successfully", foreground="green")
                
            except Exception as e:
                self.info_label.config(text=f"Error: {e}", foreground="red")
        
        threading.Thread(target=generate, daemon=True).start()
    
    def update_preview(self, sticker_image):
        """Update sticker preview"""
        try:
            # Resize for preview (maintain aspect ratio)
            display_size = (300, 350)
            sticker_resized = sticker_image.copy()
            sticker_resized.thumbnail(display_size, Image.Resampling.LANCZOS)
            
            # Convert to PhotoImage
            photo = ImageTk.PhotoImage(sticker_resized)
            self.preview_label.config(image=photo, text="")
            self.preview_label.image = photo  # Keep a reference
        except Exception as e:
            print(f"Error updating preview: {e}")
    
    def save_sticker(self):
        """Save the generated sticker"""
        if not self.sticker_image or not self.current_data:
            messagebox.showwarning("No Sticker", "No sticker to save")
            return
        
        try:
            sticker_type = self.current_data['type']
            sticker_id = self.current_data['id']
            
            if sticker_type == 'hardware':
                filename = f"{sticker_id}-label.jpg"
            else:  # location
                filename = f"{sticker_id}-Location-label.jpg"
            
            filepath = self.qr_gen.save_sticker(self.sticker_image, filename)
            if filepath:
                self.info_label.config(text=f"Sticker saved: {filename}", foreground="green")
                
                # Ask if user wants to open the saved file
                if messagebox.askyesno("Sticker Saved", f"Sticker saved successfully!\n\nOpen the saved file?"):
                    try:
                        if sys.platform.startswith('win'):
                            os.startfile(filepath)
                        elif sys.platform.startswith('darwin'):  # macOS
                            subprocess.run(['open', filepath])
                        else:  # Linux
                            subprocess.run(['xdg-open', filepath])
                    except Exception as e:
                        messagebox.showinfo("Info", f"File saved at: {filepath}")
            else:
                self.info_label.config(text="Failed to save sticker", foreground="red")
                
        except Exception as e:
            messagebox.showerror("Error", f"Failed to save sticker: {e}")
    
    def _extract_id(self, combo_text):
        """Extract ID from combo box text like 'Name (ID: 123)'"""
        try:
            return combo_text.split('ID: ')[1].rstrip(')')
        except:
            return ""

class SearchItemDialog(BaseDialog):
    """Search items dialog - replaces search.cs"""
    
    def __init__(self, parent, config, api_client, list_mgr):
        self.config = config
        self.api_client = api_client
        self.list_mgr = list_mgr
        self.search_results = []
        super().__init__(parent, "ELW - Search", (600, 500))
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Search frame
        search_frame = ttk.Frame(main_frame)
        search_frame.pack(fill=tk.X, pady=(0, 10))
        
        ttk.Label(search_frame, text="Search:").pack(side=tk.LEFT)
        self.search_entry = ttk.Entry(search_frame, width=40)
        self.search_entry.pack(side=tk.LEFT, padx=(5, 0), fill=tk.X, expand=True)
        self.search_entry.bind('<Return>', lambda e: self.search())
        
        ttk.Button(search_frame, text="Go", command=self.search).pack(side=tk.LEFT, padx=(5, 0))
        
        # Results
        ttk.Label(main_frame, text="Results:").pack(anchor=tk.W)
        
        # Results listbox with scrollbar
        results_frame = ttk.Frame(main_frame)
        results_frame.pack(fill=tk.BOTH, expand=True, pady=(0, 10))
        
        self.results_listbox = tk.Listbox(results_frame)
        scrollbar = ttk.Scrollbar(results_frame, orient=tk.VERTICAL, command=self.results_listbox.yview)
        self.results_listbox.config(yscrollcommand=scrollbar.set)
        
        self.results_listbox.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        scrollbar.pack(side=tk.RIGHT, fill=tk.Y)
        
        self.results_listbox.bind('<<ListboxSelect>>', self.on_select)
        
        # Selected item info
        self.selected_label = ttk.Label(main_frame, text="Selected Item ID", 
                                       background="white", relief=tk.SUNKEN)
        self.selected_label.pack(fill=tk.X, pady=5)
        
        # Button frame
        btn_frame = ttk.Frame(main_frame)
        btn_frame.pack(fill=tk.X, pady=10)
        
        ttk.Button(btn_frame, text="Get Info", command=self.get_info).pack(side=tk.LEFT)
        ttk.Button(btn_frame, text="Add to list", command=self.add_to_list).pack(side=tk.LEFT, padx=(5, 0))
        ttk.Button(btn_frame, text="Back To Main Menu", command=self.close).pack(side=tk.RIGHT)
        
        # Status info
        ttk.Label(main_frame, text="Keyboard Shortcuts Disabled! - Use the keypad for data entry!", 
                 font=('Arial', 6)).pack(side=tk.BOTTOM, anchor=tk.E)
    
    def search(self):
        """Search for items"""
        search_term = self.search_entry.get().strip()
        if not search_term:
            messagebox.showerror("Error", "Search term is required")
            return
        
        def do_search():
            try:
                self.selected_label.config(text="Searching...")
                self.search_results = self.api_client.search_hardware(search_term)
                
                # Update results listbox
                self.results_listbox.delete(0, tk.END)
                if self.search_results:
                    for item in self.search_results:
                        name = item.get('name', 'Unknown')
                        self.results_listbox.insert(tk.END, name)
                    self.selected_label.config(text=f"Found {len(self.search_results)} items")
                else:
                    self.selected_label.config(text="No result found")
                
            except Exception as e:
                self.selected_label.config(text=f"Search error: {e}")
        
        threading.Thread(target=do_search, daemon=True).start()
    
    def on_select(self, event=None):
        """Handle selection in results listbox"""
        selection = self.results_listbox.curselection()
        if selection:
            index = selection[0]
            if index < len(self.search_results):
                item = self.search_results[index]
                item_id = item.get('id', 'Unknown')
                self.selected_label.config(text=f"ID: {item_id}")
    
    def get_info(self):
        """Get detailed info for selected item"""
        selection = self.results_listbox.curselection()
        if not selection:
            messagebox.showwarning("No Selection", "Please select an item first")
            return
        
        index = selection[0]
        if index < len(self.search_results):
            item = self.search_results[index]
            item_id = item.get('id', '')
            
            # Open read info dialog
            ReadInfoDialog(self.window, self.config, self.api_client, self.list_mgr, 
                          item_id, "Back to search")
    
    def add_to_list(self):
        """Add selected item to list"""
        selection = self.results_listbox.curselection()
        if not selection:
            messagebox.showwarning("No Selection", "Please select an item first")
            return
        
        index = selection[0]
        if index < len(self.search_results):
            item = self.search_results[index]
            
            def add():
                try:
                    # Get detailed item info
                    item_id = item.get('id', '')
                    detailed_item = self.api_client.get_hardware(item_id)
                    
                    if detailed_item:
                        name = detailed_item.get('name', 'Unknown')
                        asset_tag = detailed_item.get('asset_tag', '')
                        location = detailed_item.get('rtd_location', {})
                        location_name = location.get('name', 'Unknown Location') if location else 'Unknown Location'
                        
                        if self.list_mgr.add_to_list(location_name, asset_tag, name):
                            self.selected_label.config(text=f"{item_id} Added to list!")
                        else:
                            self.selected_label.config(text="Failed to add to list")
                    else:
                        self.selected_label.config(text="Failed to get item details")
                        
                except Exception as e:
                    self.selected_label.config(text=f"Error: {e}")
            
            threading.Thread(target=add, daemon=True).start()

class ReadInfoDialog(BaseDialog):
    """Read information dialog - replaces ReadInfo.cs and ReadInfo2.cs"""
    
    def __init__(self, parent, config, api_client, list_mgr, initial_id="", exit_text="Back To Main Menu"):
        self.config = config
        self.api_client = api_client
        self.list_mgr = list_mgr
        self.current_data = None
        self.exit_text = exit_text
        super().__init__(parent, "ELW - Informations", (550, 650))
        
        if initial_id:
            self.url_entry.insert(0, initial_id)
            self.get_info()
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # URL/ID entry
        url_frame = ttk.Frame(main_frame)
        url_frame.pack(fill=tk.X, pady=(0, 10))
        
        self.url_entry = ttk.Entry(url_frame, width=40)
        self.url_entry.pack(side=tk.LEFT, fill=tk.X, expand=True)
        self.url_entry.bind('<Return>', lambda e: self.get_info())
        
        ttk.Button(url_frame, text="GET", command=self.get_info).pack(side=tk.LEFT, padx=(5, 0))
        
        # Info fields
        info_frame = ttk.LabelFrame(main_frame, text="Item Information", padding="10")
        info_frame.pack(fill=tk.X, pady=(0, 10))
        
        fields = [
            ("Name:", "name"),
            ("ID:", "asset_tag"),
            ("Location:", "location"),
            ("Category:", "category")
        ]
        
        self.info_fields = {}
        for i, (label_text, field_key) in enumerate(fields):
            ttk.Label(info_frame, text=label_text).grid(row=i, column=0, sticky=tk.W, pady=2)
            field_label = ttk.Label(info_frame, text="", background="white", relief=tk.SUNKEN)
            field_label.grid(row=i, column=1, sticky=tk.EW, padx=(5, 0), pady=2)
            self.info_fields[field_key] = field_label
        
        info_frame.columnconfigure(1, weight=1)
        
        # Custom fields
        custom_frame = ttk.LabelFrame(main_frame, text="Custom Fields", padding="10")
        custom_frame.pack(fill=tk.BOTH, expand=True, pady=(0, 10))
        
        self.custom_listbox = tk.Listbox(custom_frame, height=6)
        custom_scrollbar = ttk.Scrollbar(custom_frame, orient=tk.VERTICAL, command=self.custom_listbox.yview)
        self.custom_listbox.config(yscrollcommand=custom_scrollbar.set)
        
        self.custom_listbox.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        custom_scrollbar.pack(side=tk.RIGHT, fill=tk.Y)
        
        # Status
        self.status_label = ttk.Label(main_frame, text="Waiting for user...", foreground="blue")
        self.status_label.pack(pady=5)
        
        # Button frame
        btn_frame = ttk.Frame(main_frame)
        btn_frame.pack(fill=tk.X, pady=10)
        
        ttk.Button(btn_frame, text="Add to List", command=self.add_to_list).pack(side=tk.LEFT)
        ttk.Button(btn_frame, text=self.exit_text, command=self.close).pack(side=tk.RIGHT)
        
        # Status info
        ttk.Label(main_frame, text="Keyboard Shortcuts Disabled! - Use the keypad for data entry!", 
                 font=('Arial', 6)).pack(side=tk.BOTTOM, anchor=tk.E)
    
    def get_info(self):
        """Get item information"""
        item_input = self.url_entry.get().strip()
        if not item_input:
            messagebox.showerror("Error", "Item URL or ID is required")
            return
        
        def fetch_data():
            try:
                self.status_label.config(text="Loading information...", foreground="blue")
                
                # Extract ID from URL if needed
                server_address = self.config.get("ServerAddress", "").rstrip('/')
                url_pattern = f"{server_address}/hardware/"
                
                item_id = item_input
                if url_pattern in item_input:
                    item_id = item_input.replace(url_pattern, "")
                
                # Get item data
                data = self.api_client.get_hardware(item_id)
                if data:
                    self.current_data = data
                    self.display_info(data)
                else:
                    self.status_label.config(text="Item not found", foreground="red")
                    
            except Exception as e:
                self.status_label.config(text=f"Error: {e}", foreground="red")
        
        threading.Thread(target=fetch_data, daemon=True).start()
    
    def display_info(self, data):
        """Display item information"""
        try:
            # Basic info
            self.info_fields["name"].config(text=data.get("name", ""))
            self.info_fields["asset_tag"].config(text=data.get("asset_tag", ""))
            
            # Location
            location = data.get("rtd_location", {})
            location_name = location.get("name", "") if location else ""
            self.info_fields["location"].config(text=location_name)
            
            # Category
            category = data.get("category", {})
            category_name = category.get("name", "") if category else ""
            self.info_fields["category"].config(text=category_name)
            
            # Custom fields
            self.custom_listbox.delete(0, tk.END)
            custom_fields = data.get("custom_fields", {})
            if custom_fields:
                for field_name, field_data in custom_fields.items():
                    if isinstance(field_data, dict):
                        value = field_data.get("value", "")
                        if value:
                            self.custom_listbox.insert(tk.END, f"{field_name}: {value}")
                    else:
                        if field_data:
                            self.custom_listbox.insert(tk.END, f"{field_name}: {field_data}")
            
            if self.custom_listbox.size() == 0:
                self.custom_listbox.insert(tk.END, "No custom fields for this item")
            
            self.status_label.config(text="Information loaded successfully", foreground="green")
        except Exception as e:
            self.status_label.config(text=f"Error displaying info: {e}", foreground="red")
    
    def add_to_list(self):
        """Add current item to list"""
        if not self.current_data:
            messagebox.showwarning("No Data", "No item information loaded")
            return
        
        try:
            name = self.current_data.get("name", "Unknown")
            asset_tag = self.current_data.get("asset_tag", "")
            location = self.current_data.get("rtd_location", {})
            location_name = location.get("name", "Unknown Location") if location else "Unknown Location"
            
            if self.list_mgr.add_to_list(location_name, asset_tag, name):
                self.status_label.config(text="Added to list successfully!", foreground="green")
            else:
                self.status_label.config(text="Failed to add to list", foreground="red")
        except Exception as e:
            messagebox.showerror("Error", f"Failed to add to list: {e}")

class ShowListDialog(BaseDialog):
    """Show item list dialog"""
    
    def __init__(self, parent, list_mgr):
        self.list_mgr = list_mgr
        super().__init__(parent, "ELW - Item List", (700, 500))
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="10")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Text area to show list
        self.text_area = scrolledtext.ScrolledText(main_frame, wrap=tk.WORD)
        self.text_area.pack(fill=tk.BOTH, expand=True, pady=(0, 10))
        
        # Load list contents
        contents = self.list_mgr.get_list_contents()
        if contents:
            self.text_area.insert(tk.END, contents)
        else:
            self.text_area.insert(tk.END, "No items in list yet.")
        
        # Button frame
        btn_frame = ttk.Frame(main_frame)
        btn_frame.pack(fill=tk.X)
        
        ttk.Button(btn_frame, text="Clear List", command=self.clear_list).pack(side=tk.LEFT)
        ttk.Button(btn_frame, text="Export List", command=self.export_list).pack(side=tk.LEFT, padx=(5, 0))
        ttk.Button(btn_frame, text="Close", command=self.close).pack(side=tk.RIGHT)
    
    def clear_list(self):
        """Clear the list"""
        if messagebox.askyesno("Confirm", "Are you sure you want to clear the list?"):
            if self.list_mgr.clear_list():
                self.text_area.delete(1.0, tk.END)
                self.text_area.insert(tk.END, "List cleared.")
                messagebox.showinfo("Success", "List cleared successfully!")
    
    def export_list(self):
        """Export list to file"""
        filename = filedialog.asksaveasfilename(
            title="Export List",
            defaultextension=".txt",
            filetypes=[("Text files", "*.txt"), ("All files", "*.*")]
        )
        if filename:
            try:
                contents = self.text_area.get(1.0, tk.END)
                with open(filename, 'w', encoding='utf-8') as f:
                    f.write(contents)
                messagebox.showinfo("Success", f"List exported to {filename}")
            except Exception as e:
                messagebox.showerror("Error", f"Failed to export list: {e}")

# ==================== MAIN APPLICATION CLASS ====================

class MainMenuWindow:
    """Main menu window - replaces MainMenu.cs"""
    
    def __init__(self, config, api_client, qr_gen, list_mgr):
        self.config = config
        self.api_client = api_client
        self.qr_gen = qr_gen
        self.list_mgr = list_mgr
        self.window = tk.Toplevel()
        self.window.title("ELW - Main Menu")
        self.window.geometry("500x600")
        self.create_widgets()
        
        # Early connect if enabled
        if self.config.get("EarlyConnect", False):
            self.early_connect()
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.window, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Title
        title_label = ttk.Label(main_frame, text="Emily's Little Warehouse",
                               font=('Arial', 14, 'bold'))
        title_label.pack(pady=10)
        
        # Cat image placeholder
        cat_frame = ttk.Frame(main_frame, relief=tk.RAISED, borderwidth=2,
                             width=200, height=150)
        cat_frame.pack(pady=20)
        cat_frame.pack_propagate(False)
        cat_label = ttk.Label(cat_frame, text="🐱", font=('Arial', 48))
        cat_label.place(relx=0.5, rely=0.5, anchor=tk.CENTER)
        
        # Menu buttons
        buttons = [
            ("1 - Read Information", self.read_info),
            ("2 - Add New Items", self.add_items),
            ("3 - Move Item", self.move_item),
            ("4 - Move Item in Batch", self.batch_move),
            ("5 - Create Locations", self.create_location),
            ("6 - Generate Stickers", self.generate_stickers),
            ("7 - Search Item", self.search_item),
            ("8 - Show List", self.show_list),
            ("9 - Exit", self.exit_app)
        ]
        
        for i, (text, command) in enumerate(buttons):
            btn = ttk.Button(main_frame, text=text, command=command)
            btn.pack(fill=tk.X, pady=2)
        
        # Keyboard shortcuts info
        ttk.Label(main_frame, text="Keyboard Shortcuts Enabled! - Use the keypad for navigation!",
                 font=('Arial', 6)).pack(side=tk.BOTTOM, anchor=tk.E, pady=5)
    
    def early_connect(self):
        """Test early connection to server"""
        def test():
            try:
                self.api_client.test_connection()
            except:
                messagebox.showwarning("Connection", "Connection timed out - Make sure you're properly connected before continuing!")
        
        threading.Thread(target=test, daemon=True).start()
    
    def read_info(self):
        """Open read information dialog"""
        ReadInfoDialog(self.window, self.config, self.api_client, self.list_mgr)
    
    def add_items(self):
        """Open add items dialog"""
        AddItemDialog(self.window, self.config, self.api_client)
    
    def move_item(self):
        """Open move item dialog"""
        MoveItemDialog(self.window, self.config, self.api_client)
    
    def batch_move(self):
        """Open batch move dialog"""
        BatchMoveDialog(self.window, self.config, self.api_client)
    
    def create_location(self):
        """Open create location dialog"""
        AddLocationDialog(self.window, self.config, self.api_client)
    
    def generate_stickers(self):
        """Open sticker generation dialog"""
        GenerateStickersDialog(self.window, self.config, self.api_client, self.qr_gen)
    
    def search_item(self):
        """Open search dialog"""
        SearchItemDialog(self.window, self.config, self.api_client, self.list_mgr)
    
    def show_list(self):
        """Open show list dialog"""
        ShowListDialog(self.window, self.list_mgr)
    
    def exit_app(self):
        """Exit application"""
        self.window.destroy()

class EmilyLittleWarehouse:
    """Main application class - replaces Form1.cs and Program.cs"""
    
    def __init__(self):
        self.root = tk.Tk()
        self.root.title("Emily's Little Warehouse")
        self.root.geometry("500x600")
        
        # Initialize components
        self.config = ConfigManager()
        self.api_client = SnipeITClient(self.config)
        self.qr_gen = QRCodeGenerator(self.config)
        self.list_mgr = ListManager(self.config)
        
        self.create_widgets()
        self.setup_keybindings()
    
    def create_widgets(self):
        main_frame = ttk.Frame(self.root, padding="20")
        main_frame.pack(fill=tk.BOTH, expand=True)
        
        # Cat image placeholder (clickable)
        cat_frame = ttk.Frame(main_frame, relief=tk.RAISED, borderwidth=2,
                             width=230, height=206)
        cat_frame.pack(pady=20)
        cat_frame.pack_propagate(False)
        
        cat_label = ttk.Label(cat_frame, text="🐱", font=('Arial', 64), cursor="hand2")
        cat_label.place(relx=0.5, rely=0.5, anchor=tk.CENTER)
        cat_label.bind("<Button-1>", lambda e: self.open_main_menu())
        
        # Instructions
        ttk.Label(main_frame, text="1 - Press Cat To Login",
                 font=('Arial', 12)).pack(pady=10)
        
        # Menu links
        links = [
            ("2 - WiFi Setup", self.wifi_setup),
            ("3 - GSM Setup", self.gsm_setup),
            ("4 - Server Setup", self.open_server_settings),
            ("5 - Exit to the OS", self.exit_app)
        ]
        
        for text, command in links:
            link = ttk.Label(main_frame, text=text, foreground="blue", 
                           cursor="hand2", font=('Arial', 10, 'underline'))
            link.pack(anchor=tk.W, padx=20, pady=2)
            link.bind("<Button-1>", lambda e, cmd=command: cmd())
        
        # About link
        about_link = ttk.Label(main_frame, text="6 - About Emily's Little Warehouse",
                             foreground="blue", cursor="hand2", 
                             font=('Arial', 6, 'underline'))
        about_link.pack(side=tk.BOTTOM, anchor=tk.E, pady=5)
        about_link.bind("<Button-1>", lambda e: self.open_about())
        
        # Keyboard shortcuts info
        ttk.Label(main_frame, text="Keyboard Shortcuts Enabled! - Use the keypad for navigation!",
                 font=('Arial', 6)).pack(side=tk.BOTTOM, anchor=tk.E)
    
    def setup_keybindings(self):
        """Setup keyboard shortcuts"""
        self.root.bind('1', lambda e: self.open_main_menu())
        self.root.bind('2', lambda e: self.wifi_setup())
        self.root.bind('3', lambda e: self.gsm_setup())
        self.root.bind('4', lambda e: self.open_server_settings())
        self.root.bind('5', lambda e: self.exit_app())
        self.root.bind('6', lambda e: self.open_about())
        
        # Focus to enable keyboard shortcuts
        self.root.focus_set()
    
    def open_main_menu(self):
        """Open main menu"""
        # Check if server is configured
        if not self.config.get("ServerAddress") or not self.config.get("ServerKey"):
            messagebox.showwarning("Configuration Required", 
                                 "Please configure server settings first (option 4)")
            return
        
        MainMenuWindow(self.config, self.api_client, self.qr_gen, self.list_mgr)
    
    def wifi_setup(self):
        """Open WiFi setup (placeholder)"""
        messagebox.showinfo("WiFi Setup", "WiFi configuration is handled by your system.\n\nOn Windows: Use Network & Internet settings\nOn macOS: Use Network preferences\nOn Linux: Use NetworkManager or system network tools")
    
    def gsm_setup(self):
        """Open GSM setup (placeholder)"""
        messagebox.showinfo("GSM Setup", "GSM/Cellular configuration is handled by your system.\n\nPlease use your system's network configuration tools.")
    
    def open_server_settings(self):
        """Open server settings dialog"""
        ServerSettingsDialog(self.root, self.config, self.api_client)
    
    def open_about(self):
        """Open about dialog"""
        AboutDialog(self.root)
    
    def exit_app(self):
        """Exit application"""
        self.root.quit()
    
    def run(self):
        """Run the application"""
        self.root.mainloop()

# ==================== MAIN FUNCTION ====================

def check_dependencies():
    """Check if required dependencies are installed"""
    missing = []
    
    try:
        import requests
    except ImportError:
        missing.append("requests")
    
    try:
        import qrcode
    except ImportError:
        missing.append("qrcode")
    
    try:
        from PIL import Image, ImageDraw, ImageFont, ImageTk
    except ImportError:
        missing.append("Pillow")
    
    if missing:
        deps_str = ", ".join(missing)
        print(f"Missing dependencies: {deps_str}")
        print(f"Install with: pip install {' '.join(missing)}")
        return False
    
    return True

def main():
    """Main entry point"""
    try:
        if not check_dependencies():
            input("Press Enter to exit...")
            return
            
        app = EmilyLittleWarehouse()
        app.run()
    except Exception as e:
        print(f"Error starting application: {e}")
        try:
            messagebox.showerror("Error", f"Failed to start application:\n{e}")
        except:
            pass  # Might fail if tkinter isn't available

if __name__ == "__main__":
    main()